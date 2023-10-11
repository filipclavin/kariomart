using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    private PauseMenuManager pauseMenuManager;

    [SerializeField] private Button _menuButtonPrefab;
    [SerializeField] private float _buttonDistance;
    [SerializeField] private Button _onePlayerButton;
    [SerializeField] private Button _twoPlayerButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuManager = FindAnyObjectByType<PauseMenuManager>();

        if (pauseMenuManager != null)
        {
            pauseMenuManager.enabled = false;
        }

        if (_gameData.playerCount == 1)
        {
            _onePlayerButton.Select();
        } else
        {
            _twoPlayerButton.Select();
        }

        _onePlayerButton.onClick.AddListener(() => {
            _gameData.playerCount = 1;
        });
        _twoPlayerButton.onClick.AddListener(() => {
            _gameData.playerCount = 2;
        });

        CreateMapButtons(_gameData.trackScenes);
    }

    private void Update()
    {

    }

    private void CreateMapButtons(List<SceneAsset> trackScenes)
    {
        for (int i = 0; i < trackScenes.Count; i++)
        {
            string sceneName = trackScenes[i].name;

            Button sceneButton = Instantiate(_menuButtonPrefab, transform);
            sceneButton.name = "Load " + sceneName + "Button";
            sceneButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;

            Vector3 pos = sceneButton.transform.position;
            sceneButton.transform.position = new Vector3(pos.x, pos.y - _buttonDistance * (i + 1));

            sceneButton.onClick.AddListener(() => GameManager.Instance.LoadTrack(sceneName));
        }

    }

}
