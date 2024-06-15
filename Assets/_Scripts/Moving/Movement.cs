using UnityEngine;

public class Movement {
    private Transform _movingObject;

    public Movement(Transform movingObject) {
        _movingObject = movingObject;
    }

    public void Move(float speed) {
        _movingObject.position += speed * _movingObject.forward * Time.deltaTime;
    }
}