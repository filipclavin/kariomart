using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameData gameData;

    public bool twoPlayerMode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadTrack(string trackName)
    {
        // TODO: Add transition

        SceneManager.LoadScene(trackName);
        SceneManager.LoadScene(gameData.pauseMenuScene.name, LoadSceneMode.Additive);
    }

    public void LoadMainMenu()
    {
        // TODO: Add transition

        SceneManager.LoadScene(gameData.mainMenuScene.name);
    }

}
