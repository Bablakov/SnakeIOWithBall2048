using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable {
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private KillField _killField;
    private ButtonSoundControl _buttonSoundControl;
    private LeaderBoard _leaderBoard;

    public void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    private void GetComponents() {
        _viewNumberKilledEnemy = GetComponentInChildren<ViewNumberKilledEnemy>();
        _buttonSoundControl = GetComponentInChildren<ButtonSoundControl>();
        _leaderBoard = GetComponentInChildren<LeaderBoard>();
        _killField = GetComponentInChildren<KillField>();
    }

    private void InitializeComponents() {
        _viewNumberKilledEnemy.Initialize();
        _buttonSoundControl.Initialize();
        _leaderBoard.Initailize();
        _killField.Initialize();
    }
}