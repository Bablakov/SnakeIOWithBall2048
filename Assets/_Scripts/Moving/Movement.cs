using UnityEngine;
using UnityEngine.AI;

public class Movement {
    private Transform _movingObject;
    private Section _seciton;

    public Movement(Transform movingObject, Section section) {
        _movingObject = movingObject;
        _seciton = section;
    }

    public void Move(float speed) {
        var pointMovement = speed * _movingObject.forward * Time.deltaTime;
        var pointMovementX = speed * new Vector3(_movingObject.forward.x, 0, 0) * Time.deltaTime;
        var pointMovementZ = speed * new Vector3(0, 0, _movingObject.forward.z) * Time.deltaTime;

        if (NavMesh.SamplePosition(pointMovement + _seciton.PositionFront, out _, 0f, NavMesh.AllAreas)) {
            _movingObject.position += pointMovement;
        } 
        else if (NavMesh.SamplePosition(pointMovementX + _movingObject.position, out _, 0f, NavMesh.AllAreas)) {
            _movingObject.position += pointMovementX;
        } 
        else if (NavMesh.SamplePosition(pointMovementZ + _movingObject.position, out _, 0f, NavMesh.AllAreas)) {
            _movingObject.position += pointMovementZ;
        }
        
    }
}