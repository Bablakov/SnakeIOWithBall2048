using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowingElements {
    private ParametrsSnake _parametrsPlayer;
    private StorageSection _storageSection;
    private IReadOnlyCollection<Section> bodyElements => _storageSection.Sections;

    public FollowingElements(ParametrsSnake parametrsPlayer, StorageSection storageSection) {
        _parametrsPlayer = parametrsPlayer;
        _storageSection = storageSection;
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
        var backPartNextSection = bodyElements.ElementAt(index - 1).PositionBack;
        var coeficientMovement = CalculateCoefficient(index);
        RotateBody(index, backPartNextSection);
        MoveBody(index, coeficientMovement);
    }

    private float CalculateCoefficient(int index) {
        return (GetTotalSizeBeetwenCenterSection(index) + _parametrsPlayer.DistanceBeetwenSection) / GetLengthBeetwenSection(index);
    }

    private void RotateBody(int index, Vector3 backNext) {
        bodyElements.ElementAt(index).transform.LookAt(backNext);
    }

    private void MoveBody(int index, float coefficient) {
        bodyElements.ElementAt(index).transform.position += GetDirectionMove(index) * _parametrsPlayer.Speed / coefficient * Time.deltaTime;
    }

    private float GetLengthBeetwenSection(int index) {
        return (bodyElements.ElementAt(index - 1).Position - bodyElements.ElementAt(index).Position).magnitude;
    }

    private float GetTotalSizeBeetwenCenterSection(int index) {
        return bodyElements.ElementAt(index).Width + bodyElements.ElementAt(index - 1).Width;
    }

    private Vector3 GetDirectionMove(int index) {
        return (bodyElements.ElementAt(index - 1).Position - bodyElements.ElementAt(index).Position).normalized;
    }
}