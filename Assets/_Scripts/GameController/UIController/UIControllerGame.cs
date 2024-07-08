public class UIControllerGame : UIController {
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private KillField _killField;
    private LeaderBoard _leaderBoard;

    protected override void GetComponents() {
        base.GetComponents();
        _viewNumberKilledEnemy = GetComponentInChildren<ViewNumberKilledEnemy>();
        _leaderBoard = GetComponentInChildren<LeaderBoard>();
        _killField = GetComponentInChildren<KillField>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _viewNumberKilledEnemy.Initialize();
        _leaderBoard.Initailize();
        _killField.Initialize();
    }
}