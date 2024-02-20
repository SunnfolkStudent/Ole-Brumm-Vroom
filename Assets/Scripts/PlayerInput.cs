using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls _playerControls;
    private void Awake() => _playerControls = new PlayerControls();
    private void OnEnable() => _playerControls.Enable();
    private void OnDisable() => _playerControls.Disable();

    // Player ActionMap Controls
    public static bool Jump;
    public static bool SlowDescend;
    public static bool DropBelow;
    public static bool OpenPauseScreen;

    // ResetRun ActionMap Controls:
    public static bool ResetRun;
    
    // PauseScreen ActionMap Controls:
    public static bool QuitGame;
    public static bool ClosePauseScreen;

    public void ChangeInputToResetRun()
    {
        OnDisable();
        _playerControls.ResetRun.Enable();
    }

    public void ChangeToPlayer()
    {
        OnDisable();
        _playerControls.Player.Enable();
    }
    
    public void ChangeToPauseScreen()
    {
        OnDisable();
        _playerControls.PauseScreen.Enable();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Player ActionMap Controls:
        Jump = _playerControls.Player.Jump.triggered;
        SlowDescend = _playerControls.Player.Float.triggered;
        DropBelow = _playerControls.Player.DropBelow.triggered;
        OpenPauseScreen = _playerControls.Player.OpenPauseScreen.triggered;

        // ResetRun ActionMap Controls:
        ResetRun = _playerControls.ResetRun.AnyKey.triggered;
        
        // PauseScreen ActionMap Controls:
        QuitGame = _playerControls.PauseScreen.QuitGame.triggered;
        ClosePauseScreen = _playerControls.PauseScreen.Continue.triggered;
    }
}
