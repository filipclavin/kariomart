using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    static public RaceManager Instance { get; private set; }

    [SerializeField] private GameData _gameData;
    [SerializeField] private int _laps;
    [SerializeField] private Transform _track;

    private List<GameObject> checkpoints;

    public float _timePassed = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Object[] players = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
        checkpoints = GameObject.FindGameObjectsWithTag(_gameData.checkpointTag).ToList();

        foreach (Object player in players)
        {
            _gameData.raceProgress.Add(player.name + "CheckpointsLeft", checkpoints.Count);
            _gameData.raceProgress.Add(player.name + "LapsLeft", _laps);
        }
    }

    public void PassCheckpoint(GameObject player, GameObject checkpoint)
    {
        checkpoints.Remove(checkpoint);
        _gameData.raceProgress[player.name + "CheckpointsLeft"] = checkpoints.Count;
    }

    public void CompleteLap(GameObject player)
    {
        _gameData.raceProgress[player.name + "LapsLeft"]--;

        if (_gameData.raceProgress[player.name + "LapsLeft"] == 0)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed = Time.time;
    }
}
