using UnityEngine;
using Zenject;

public class TestPoolScript : MonoBehaviour {
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    private SectionPool _sectionPool;

    [Inject]
    private void Construct(SectionPool sectionPool) {
        _sectionPool = sectionPool;
        Debug.Log("Construct");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var go = _sectionPool.Spawn();
            go.transform.position = GetRandomPosition();
            Debug.Log($"Spawn {go}, {go.transform.position}");
        }
    }

    private Vector3 GetRandomPosition() {
        var randomX = Random.Range(_minX, _maxX);
        var randomY = Random.Range(_minY, _maxY);
        return new Vector3(randomX, 0, randomY);
    }

}