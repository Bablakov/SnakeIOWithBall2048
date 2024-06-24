using UnityEngine;

public class UIController : MonoBehaviour {
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private KillField _killField;
    private ButtonSoundControl _buttonSoundControl;
    private LeaderBoard _leaderBoard;

    public void Initialize() {
        _viewNumberKilledEnemy = GetComponentInChildren<ViewNumberKilledEnemy>();
        _buttonSoundControl = GetComponentInChildren<ButtonSoundControl>();
        _leaderBoard = GetComponentInChildren<LeaderBoard>();
        _killField = GetComponentInChildren<KillField>();

        _viewNumberKilledEnemy.Initialize();
        _buttonSoundControl.Initialize();
        _leaderBoard.Initailize();
        _killField.Initialize();
    }
}