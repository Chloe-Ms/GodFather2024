using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardWorldMovement : MonoBehaviour
{
    [SerializeField] float _forwardMaxSpeed = 10f;
    [SerializeField] float _timeToAccelerate = 2f;
    [SerializeField] float _timeToDeccelerate = 2f;
    [SerializeField] GameObject _normalChunk;
    [SerializeField] GameObject _worldParent;
    public List<GameObject> ObjectsToMove { get; private set; }

    Camera _cam;
    Coroutine _coroutine;
    float _speed = 0f;
    ManagerRoad _managerRoad;
    int _indexNextChunk;

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
                chunkGameobject = _normalChunk;
            }
            var instance = Instantiate(chunkGameobject, _worldParent.transform.position + _cam.transform.forward * _managerRoad.ChunkSize * i, Quaternion.identity, _worldParent.transform);
            ObjectsToMove.Add(instance);
        }
        
        _indexNextChunk = _managerRoad.NbOfChunksPreloaded + 1;
    }

    IEnumerator RoutineAccelerate()
    {
        float timer = 0f;
        while (timer < _timeToAccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(0f, _forwardMaxSpeed, timer / _timeToAccelerate);
            yield return null;
        }
        _speed = _forwardMaxSpeed;
    }

    public void StartMovingForward()
    {
        if (_coroutine == null && _speed != _forwardMaxSpeed)
        {
            _coroutine = StartCoroutine(RoutineAccelerate());
        }
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
        while (timer < _timeToAccelerate)
        {
            timer += Time.deltaTime;
            _speed = Mathf.Lerp(_forwardMaxSpeed, 0f, timer / _timeToDeccelerate);
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
                    chunkGameobject = _normalChunk;
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
