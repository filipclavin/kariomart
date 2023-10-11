using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    static public RaceManager Instance { get; private set; }

    [SerializeField] private GameData _gameData;
    [SerializeField] private int _laps;
    [SerializeField] private GameObject _boostPrefab;
    [SerializeField] private int _maxBoostAmount;
    [SerializeField] private float _boostSpawnInterval;
    [SerializeField] private GameObject playerTwo;

    private List<GameObject> _checkpoints = new();

    private GameObject _winUI;
    private TextMeshProUGUI _winnerTMP;
    private TextMeshProUGUI _timerTMP;

    private List<GameObject> _boostPool = new();
    private List<Transform> _boostSpawnPositions = new();

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
        _timerTMP = GameObject.FindGameObjectWithTag(_gameData.timerTMPTag).GetComponent<TextMeshProUGUI>();

        _gameData.raceProgress = new Dictionary<string, int>();

        _checkpoints = GameObject.FindGameObjectsWithTag(_gameData.checkpointTag).ToList();

        _winUI = GameObject.FindGameObjectWithTag(_gameData.winUITag);
        _winnerTMP = GameObject.FindGameObjectWithTag(_gameData.winnerTMPTag).GetComponent<TextMeshProUGUI>();
        _winUI.GetComponentInChildren<Button>().onClick.AddListener(() => GameManager.Instance.LoadMainMenu());

        _winUI.SetActive(false);

        Object[] players = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        foreach (Object player in players)
        {
            _gameData.raceProgress.Add(player.name + "CheckpointsLeft", _checkpoints.Count);
            _gameData.raceProgress.Add(player.name + "LapsLeft", _laps);
        }

        if (!(_gameData.playerCount == 2))
        {
            Destroy(playerTwo);
        }

        GameObject[] trackParts = GameObject.FindGameObjectsWithTag(_gameData.trackPartTag);
        _boostSpawnPositions = trackParts.OrderBy(x => Random.value).Select(x => x.transform).ToList();
        _boostSpawnPositions.RemoveRange(_maxBoostAmount, _boostSpawnPositions.Count - _maxBoostAmount);

        InvokeRepeating(nameof(GenerateBoosts), 0f, _boostSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void PassCheckpoint(string playerName, GameObject checkpoint)
    {
        _checkpoints.Remove(checkpoint);
        _gameData.raceProgress[playerName + "CheckpointsLeft"] = _checkpoints.Count;
    }

    public void CompleteLap(string playerName)
    {
        _gameData.raceProgress[playerName + "LapsLeft"]--;

        if (_gameData.raceProgress[playerName + "LapsLeft"] == 0)
        {
            Time.timeScale = 0;
            _winUI.SetActive(true);
            _winnerTMP.text = playerName + " wins!";
        } else
        {
            _checkpoints = GameObject.FindGameObjectsWithTag(_gameData.checkpointTag).ToList();
            _gameData.raceProgress[playerName + "CheckpointsLeft"] = _checkpoints.Count;
        }

    }

    private void UpdateTimer()
    {
        int minutes = (int)Time.time / 60;
        int seconds = (int)Time.time % 60;
        int milliseconds = (int)(Time.time * 100) % 100;

        _timerTMP.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }

    private void GenerateBoosts() {

        foreach (Transform spawnPos in _boostSpawnPositions)
        {
            GameObject boost = Instantiate(_boostPrefab, spawnPos);
            _boostPool.Add(boost);

            if (_boostPool.Count > _maxBoostAmount)
            {
                Destroy(_boostPool[0]);
                _boostPool.RemoveAt(0);
            }
        }
    }

    public void RemoveBoost(GameObject boost)
    {
        Destroy(boost);
        _boostPool.Remove(boost);
    }

}
