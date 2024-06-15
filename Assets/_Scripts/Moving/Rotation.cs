using UnityEngine;

public class Rotation {
    private Transform _rotationObject;

    public Rotation(Transform rotationObject) {
        _rotationObject = rotationObject;
    }

    public void Rotate(Vector3 direction) {
        _rotationObject.rotation = Quaternion.LookRotation(direction);
    }
}