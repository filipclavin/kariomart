using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (
                other.CompareTag(_gameData.goalTag) &&
                _gameData.raceProgress[name + "CheckpointsLeft"] == 0
           )
        {
            RaceManager.Instance.CompleteLap(name);
        }

        if (other.CompareTag(_gameData.checkpointTag))
        {
            RaceManager.Instance.PassCheckpoint(name, other.gameObject);
        }

        if (other.CompareTag(_gameData.boostTag))
        {
            _playerMovement.Boost();
            RaceManager.Instance.RemoveBoost(other.gameObject);
        }
    }
}
