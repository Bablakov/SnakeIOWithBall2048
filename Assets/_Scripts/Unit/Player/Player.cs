public class Player : Unit {
    private ControlledElement _controlledElement;

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElement = GetComponentInChildren<ControlledElement>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElement.Initialize(ParametrsSnake);
    }
}