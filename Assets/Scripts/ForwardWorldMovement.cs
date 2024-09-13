using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardWorldMovement : MonoBehaviour
{
    [SerializeField] float _forwardMaxSpeed = 10f;
    [SerializeField] float _boostSpeed = 10f;
    [SerializeField] float _timeToAccelerate = 2f;
    [SerializeField] float _timeToAccelerateBoost = 1f;
    [SerializeField] float _timeToDeccelerate = 2f;
    [SerializeField] float _timeToDeccelerateBoost = 2f;
    [SerializeField] float _boostDuration = 4f;
    [SerializeField] float _knockbackDuration = 0.5f;
    [SerializeField] float _speedKnockback = 2f;
    [SerializeField] GameObject[] _normalChunks;
    [SerializeField] GameObject _worldParent;

    Camera _cam;
    Coroutine _coroutine;
    float _speed = 0f;
    ManagerRoad _managerRoad;
    int _indexNextChunk;
    bool _isSpeedingUp = false;
    public List<GameObject> ObjectsToMove { get; private set; }
    public bool HasStarted { get; private set; } = false;
    public float ForwardMaxSpeed { get => _forwardMaxSpeed; }
    public float Speed { get => _speed; }

    private void Awake()
    {
        Managers.WorldMovement = this;
        ObjectsToMove = new List<GameObject>();
    }

    private void Start()
    {
        _cam = Camera.main;
        _managerRoad = Managers.ManagerRoad;
        for (int i = 0; i < _managerRoad.NbOfChunksPreloaded; i++)
        {
            GameObject chunkGameobject;
            if (_managerRoad.DictIndexPrefab.ContainsKey(i+1))
            {
                chunkGameobject = _managerRoad.DictIndexPrefab[i+1];
            } else
            {
                chunkGameobject = GetRandomNormalChunk();
            }
            var instance = Instantiate(chunkGameobject, _worldParent.transform.position + _cam.transform.forward * _managerRoad.ChunkSize * i, Quaternion.identity, _worldParent.transform);
            ObjectsToMove.Add(instance);
        }
        
        _indexNextChunk = _managerRoad.NbOfChunksPreloaded + 1;
    }

    GameObject GetRandomNormalChunk()
    {
        if (_normalChunks.Length == 0)
            return null;
        int randomIndex = Random.Range(0, _normalChunks.Length);
        return _normalChunks[randomIndex];
    }

    IEnumerator RoutineAccelerate()
    {
        float timer = 0f;
        while (timer < _timeToAccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(0f, ForwardMaxSpeed, timer / _timeToAccelerate);
            yield return null;
        }
        _speed = ForwardMaxSpeed;
    }

    public void StartBoost()
    {
        if (_isSpeedingUp)
            return;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        Debug.Log("START BOOST");
        _isSpeedingUp = true;
        _coroutine = StartCoroutine(RoutineAccelerateBoost());
    }

    IEnumerator RoutineAccelerateBoost()
    {
        float timer = 0f;
        float startingSpeed = _speed;
        while (_speed < _boostSpeed)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(startingSpeed, _boostSpeed, timer / _timeToAccelerateBoost);
            yield return null;
        }
        _speed = _boostSpeed;
        Debug.Log("MAX SPEED");
        _isSpeedingUp = false;
        yield return new WaitForSeconds(_boostDuration);
        _coroutine = StartCoroutine(RoutineDecelerateBoost());
        Debug.Log("END BOOST");
    }

    IEnumerator RoutineDecelerateBoost()
    {
        float timer = 0f;
        float startingSpeed = _speed;
        while (timer < _timeToDeccelerateBoost)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(startingSpeed, ForwardMaxSpeed, timer / _timeToDeccelerateBoost);
            yield return null;
        }
        _speed = ForwardMaxSpeed;
        Debug.Log("NORMAL SPEED");
    }

    public void StartMovingForward()
    {
        if (_coroutine == null && _speed != ForwardMaxSpeed)
        {
            _coroutine = StartCoroutine(RoutineAccelerate());
        }
    }

    public void Knockback()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(RoutineKnockback());
    }

    IEnumerator RoutineKnockback()
    {
        float timer = 0f;
        float startingSpeed = _speed;
        while (timer < _knockbackDuration)
        {
            timer += Time.deltaTime;
            //_speed = Mathf.Lerp(, ForwardMaxSpeed, timer / _knockbackDuration);
            yield return null;
        }
        _speed = _speedKnockback;
    }

    public void StopMovingForward()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _speed = 0f;
    }

    public void StopDecelerateMovingForward()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(RoutineDecelerate());
    }

    IEnumerator RoutineDecelerate()
    {
        float timer = 0f;
        float startingSpeed = _speed;
        while (timer < _timeToDeccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(startingSpeed, 0f, timer / _timeToDeccelerate);
            yield return null;
        }
        _speed = 0f;
    }

    void MoveForward()
    {
        if (_speed > 0f)
        {
            foreach (GameObject go in ObjectsToMove)
            {
                go.transform.localPosition += - _cam.transform.forward * _speed * Time.deltaTime;
            }
        }
    }
    //Start instead of the board
    void InputStartMoving()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HasStarted = true;
            StartMovingForward();
        }
    }

    public void UpdateChunks()
    {
        if (ObjectsToMove.Count > 0)
        {
            GameObject firstGameobject;
            firstGameobject = ObjectsToMove[0];
            ObjectsToMove.RemoveAt(0);
            Destroy(firstGameobject);
            if (_indexNextChunk <= _managerRoad.NbOfChunksTotal)
            {
                GameObject chunkGameobject;
                if (_managerRoad.DictIndexPrefab.ContainsKey(_indexNextChunk))
                {
                    chunkGameobject = _managerRoad.DictIndexPrefab[_indexNextChunk];
                }
                else
                {
                    chunkGameobject = GetRandomNormalChunk();
                }
                ObjectsToMove.Add(Instantiate(chunkGameobject, ObjectsToMove[ObjectsToMove.Count - 1].transform.position + _cam.transform.forward * _managerRoad.ChunkSize, Quaternion.identity, _worldParent.transform));
            }
            _indexNextChunk++;
        }
    }

    private void Update()
    {
        InputStartMoving();
        MoveForward();
    }
}
