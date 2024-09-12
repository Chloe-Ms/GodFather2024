using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

[Serializable]
public class RoadModifiableChunk
{
    [Min(1)]public int Index;
    public GameObject RoadChunk;
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    DIFFICULT
}

public class ManagerRoad : MonoBehaviour
{
    [SerializeField] int _nbOfChunksTotal = 50;
    [SerializeField] int _nbOfChunksPreloaded = 5;
    [SerializeField] float _chunkSize;
    [Space]
    [SerializeField] List<RoadModifiableChunk> _listPrefabsEasy;
    [SerializeField] List<RoadModifiableChunk> _listPrefabsMedium;
    [SerializeField] List<RoadModifiableChunk> _listPrefabsDifficult;

    Dictionary<int, string> _poolIndexToName;
    Dictionary<int, GameObject> _dictIndexPrefab;

    //Change for menu
    [SerializeField] Difficulty _difficulty;

    public List<RoadModifiableChunk> ListPrefabs { get => _listPrefabsEasy; }
    public float ChunkSize { get => _chunkSize; }
    public int NbOfChunksPreloaded { get => _nbOfChunksPreloaded; }
    public Dictionary<int, GameObject> DictIndexPrefab { get => _dictIndexPrefab; }
    public int NbOfChunksTotal { get => _nbOfChunksTotal; }

    private void Awake()
    {
        Managers.ManagerRoad = this;
        InstantiatePrefabsForPool(GetListPrefabsForDifficulty());
    }

    private void OnValidate()
    {
        if (_nbOfChunksPreloaded > _nbOfChunksTotal)
        {
            _nbOfChunksPreloaded = _nbOfChunksTotal;
        }
    }

    void InstantiatePrefabsForPool(List<RoadModifiableChunk> listPrefabs)
    {
        _dictIndexPrefab = new Dictionary<int, GameObject>();
        _poolIndexToName = new Dictionary<int, string>();
        foreach(var go in listPrefabs)
        {
            _poolIndexToName.Add(go.Index, go.RoadChunk.name);
            _dictIndexPrefab[go.Index] = go.RoadChunk;
        }
    }

    List<RoadModifiableChunk> GetListPrefabsForDifficulty()
    {
        switch(_difficulty)
        {
            case Difficulty.EASY: return _listPrefabsEasy;
            case Difficulty.MEDIUM: return _listPrefabsMedium;
            case Difficulty.DIFFICULT: return _listPrefabsDifficult;
            default: return null;
        }
    }
}
