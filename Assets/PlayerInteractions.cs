using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private void OnTriggerEnter(Collider other)
    {
        if (
                other.CompareTag(gameData.goalTag) &&
                gameData.raceProgress[name + "CheckpointsLeft"] == 0
           )
        {
            RaceManager.Instance.CompleteLap(gameObject);
        }

        if (other.CompareTag(gameData.checkpointTag))
        {
            RaceManager.Instance.PassCheckpoint(gameObject, other.gameObject);
        }
    }
}
