using UnityEngine;

public class InputDesktop : InputGame {
    private int HEIGHT_POINT = 0;

    private Vector3 _mousePosition;
    private int ScreenWidth => Screen.width;
    private int ScreenHeight => Screen.height;

    public override Vector3 GetDirectionMovememt() {
        GetDataInput();
        ProcessData();
        return DirectionMovement;
    }

    private void GetDataInput() {
        _mousePosition = Input.mousePosition;
    }

    private void ProcessData() {
        DirectionMovement = new Vector3(CalculatePositionByX(), HEIGHT_POINT, CalculatePositionByY());
    }

    private float CalculatePositionByX() {
        return _mousePosition.x - ScreenWidth / 2;
    }

    private float CalculatePositionByY() {
        return _mousePosition.y - ScreenHeight / 2;
    }
}