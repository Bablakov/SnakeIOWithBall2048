using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerFreeSectionOnScene : MonoBehaviour {
    [SerializeField] private float minX = -49f;
    [SerializeField] private float maxX = 49f;
    [SerializeField] private float minY = -49f;
    [SerializeField] private float maxY = 49f;
    [SerializeField] private int countFreeSection = 50;

    private bool CanSpawn => _freeSection.Count < countFreeSection;
    private int CountSpawn => countFreeSection - _freeSection.Count;

    private List<Section> _freeSection;
    private SectionPool _sectionPool;
    private SignalBus _signalBus;

    public void Initialize() {
        _freeSection = new List<Section>();
        SpawnSections();
        Subscribe();
    }

    [Inject]
    private void Construct(SectionPool sectionPool, SignalBus signaleBus) {
        _sectionPool = sectionPool;
        _signalBus = signaleBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<AddedSectionSignal>(OnAddedSection);
        _signalBus.Subscribe<ReleasedSectionSignal>(OnReleasedSection);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<AddedSectionSignal>(OnAddedSection);
        _signalBus.Unsubscribe<ReleasedSectionSignal>(OnReleasedSection);
    }

    private void OnAddedSection(AddedSectionSignal signal) {
        RemoveFromCollection(signal.SectionAdded);
        SpawnSections();
    }

    private void OnReleasedSection(ReleasedSectionSignal signal) {
        _sectionPool.Despawn(signal.SectionReleased);
    }

    private void RemoveFromCollection(Section section) {
        _freeSection.Remove(section);
    }

    private void SpawnSections() {
        if (CanSpawn) {
            SpawnMissingSection();
        }
    }

    private void SpawnMissingSection() {
        var countNeedSpawn = CountSpawn;
        for (int i = 0; i < countNeedSpawn; i++) {
            SpawnOnRandomPlaceAndAddInCollection();
        }
    }

    private void SpawnOnRandomPlaceAndAddInCollection() {
        var gameObject = _sectionPool.Spawn();
        gameObject.transform.position = GetRandomPosition();
        gameObject.transform.SetParent(transform);
        _freeSection.Add(gameObject);
    }

    private Vector3 GetRandomPosition() {
        var randomX = Random.Range(minX, maxX);
        var randomZ = Random.Range(minY, maxY);
        return new Vector3(randomX, 0, randomZ);
    }
}