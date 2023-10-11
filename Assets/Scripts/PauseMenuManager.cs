using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private Button _unpauseButton;
    [SerializeField] private Button _mainMenuButton;

    private PlayerOneActions _playerOneActions;

    // Start is called before the first frame update
    void Start()
    {
        _playerOneActions = new();
        _playerOneActions.UI.TogglePause.Enable();

        _playerOneActions.UI.TogglePause.performed += TogglePause;
        _unpauseButton.onClick.AddListener(TogglePause);

        _mainMenuButton.onClick.AddListener(GameManager.Instance.LoadMainMenu);
    }

    private void TogglePause()
    {   
        Time.timeScale = !_pauseMenuCanvas.activeSelf ? 0f : 1f;
        _pauseMenuCanvas.SetActive(!_pauseMenuCanvas.activeSelf);
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void OnDestroy()
    {
        _playerOneActions.UI.TogglePause.performed -= TogglePause;
        _playerOneActions.UI.TogglePause.Disable();
    }
}
