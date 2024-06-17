using System.Collections.Generic;
using UnityEngine;

public class FollowingElements : MonoBehaviour {
    private float _speed = 1f;
    private float _distanceBeetwen = 1f;
    private Player _player;
    private List<Section> _bodyElements => _player.bodyElements;

    public void Initialize(float speed, float distanceBeetwen, Player player/*, Section head*/) {
        _speed = speed;
        _distanceBeetwen = distanceBeetwen;
        _player = player;
        /*_bodyElements.Add(head);*/
    }

    private void Update() {
        MoveAndRotateSections();
    }

    private void MoveAndRotateSections() {
        for (int i = 1; i < _bodyElements.Count; i++) {
            MoveAndRotateSection(i);
        }
    }

    private void MoveAndRotateSection(int index) {
        var backPartNextSection = _bodyElements[index - 1].PositionBack;
        var coeficientMovement = CalculateCoefficient(index);
        RotateBody(index, backPartNextSection);
        MoveBody(index, coeficientMovement);
    }

    private float CalculateCoefficient(int index) {
        return (GetTotalSizeBeetwenCenterSection(index) + _distanceBeetwen) / GetLengthBeetwenSection(index);
    }

    private void RotateBody(int index, Vector3 backNext) {
        _bodyElements[index].transform.LookAt(backNext);
    }

    private void MoveBody(int index, float coefficient) {
        _bodyElements[index].transform.position += GetDirectionMove(index) * _speed / coefficient * Time.deltaTime;
    }

    private float GetLengthBeetwenSection(int index) {
        return (_bodyElements[index - 1].Position - _bodyElements[index].Position).magnitude;
    }

    private float GetTotalSizeBeetwenCenterSection(int index) {
        return _bodyElements[index].Width + _bodyElements[index - 1].Width;
    }

    private Vector3 GetDirectionMove(int index) {
        return (_bodyElements[index - 1].Position - _bodyElements[index].Position).normalized;
    }
}