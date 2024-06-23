using UnityEngine;
using UnityEngine.AI;

public class Movement {
    private Transform _movingObject;
    private Section _section;

    public Movement(Transform movingObject, Section section) {
        _movingObject = movingObject;
        _section = section;
    }

    public void Move(float speed) {
        var defaultMovement = CalculateDirectionMovement(_movingObject.forward, speed);
        var pointMovementX = CalculateDirectionMovement(new Vector3(_movingObject.forward.x, 0, 0), speed);
        var pointMovementZ = CalculateDirectionMovement(new Vector3(0, 0, _movingObject.forward.z), speed);

        if (CanMoveHere(pointMovementX, _section.PositionFront)) {
            Move(defaultMovement);
        } 
        else if (CanMoveHere(pointMovementX, _movingObject.position)) {
            Move(pointMovementX);
        } 
        else if (CanMoveHere(pointMovementZ, _movingObject.position)) {
            Move(pointMovementZ);
        }

    }

    private Vector3 CalculateDirectionMovement(Vector3 directionMovement, float speed) {
        return directionMovement * speed * Time.deltaTime;
    }

    private bool CanMoveHere(Vector3 directionMovement, Vector3 startMovememtPoint) {
        return NavMesh.SamplePosition(directionMovement + startMovememtPoint, out _, 0f, NavMesh.AllAreas);
    }

    private void Move(Vector3 defaultMovement) {
        _movingObject.position += defaultMovement;
    }
}