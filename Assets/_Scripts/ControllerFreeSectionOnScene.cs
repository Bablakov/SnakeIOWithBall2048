using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerFreeSectionOnScene : MonoBehaviour {
    [SerializeField] private float minX = -49f;
    [SerializeField] private float maxX = 49f;
    [SerializeField] private float minY = -49f;
    [SerializeField] private float maxY = 49f;
    [SerializeField] private int countFreeSection = 50;
    [SerializeField] private int currentCountSection;

    private List<Section> _freeSection;
    private SectionPool _sectionPool;
    private SignalBus _signalBus;

    private void Start() {
        _freeSection = new List<Section>();
        SpawnSections();
    }

    [Inject]
    private void Construct(SectionPool sectionPool, SignalBus signaleBus) {
        _sectionPool = sectionPool;
        _signalBus = signaleBus;

        _signalBus.Subscribe<AddedSectionSignal>(OnAddedSectionSignal);
    }

    private void OnAddedSectionSignal(AddedSectionSignal signal) {
        _freeSection.Remove(signal.SectionAdded);
        SpawnSections();
    }

    private void SpawnSections() {
        if (_freeSection.Count < countFreeSection) {
            var countNeedSpawn = countFreeSection - _freeSection.Count;
            for (int i = 0; i < countNeedSpawn; i++) {
                var go = _sectionPool.Spawn();
                go.transform.position = GetRandomPosition();
                _freeSection.Add(go);
            }
        }
        currentCountSection = _freeSection.Count;
    }

    private Vector3 GetRandomPosition() {
        var randomX = Random.Range(minX, maxX);
        var randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, 0, randomY);
    }
}