using System.Collections.Generic;
using UnityEngine;

public class FollowingElements {
    private ParametrsSnake _parametrsPlayer;
    private ControllerSection _controllerSection;
    private IReadOnlyList<Section> bodyElements => _controllerSection.Sections;

    public FollowingElements(ParametrsSnake parametrsPlayer, ControllerSection controllerSection) {
        _parametrsPlayer = parametrsPlayer;
        _controllerSection = controllerSection;
    }

    public void Update() {
        MoveAndRotateSections();
    }
    private void MoveAndRotateSections() {
        for (int i = 1; i < bodyElements.Count; i++) {
            MoveAndRotateSection(i);
        }
    }

    private void MoveAndRotateSection(int index) {
        var backPartNextSection = bodyElements[index - 1].PositionBack;
        var coeficientMovement = CalculateCoefficient(index);
        RotateBody(index, backPartNextSection);
        MoveBody(index, coeficientMovement);
    }

    private float CalculateCoefficient(int index) {
        return (GetTotalSizeBeetwenCenterSection(index) + _parametrsPlayer.DistanceBeetwenSection) / GetLengthBeetwenSection(index);
    }

    private void RotateBody(int index, Vector3 backNext) {
        bodyElements[index].transform.LookAt(backNext);
    }

    private void MoveBody(int index, float coefficient) {
        bodyElements[index].transform.position += GetDirectionMove(index) * _parametrsPlayer.Speed / coefficient * Time.deltaTime;
    }

    private float GetLengthBeetwenSection(int index) {
        return (bodyElements[index - 1].Position - bodyElements[index].Position).magnitude;
    }

    private float GetTotalSizeBeetwenCenterSection(int index) {
        return bodyElements[index].Width + bodyElements[index - 1].Width;
    }

    private Vector3 GetDirectionMove(int index) {
        return (bodyElements[index - 1].Position - bodyElements[index].Position).normalized;
    }
}