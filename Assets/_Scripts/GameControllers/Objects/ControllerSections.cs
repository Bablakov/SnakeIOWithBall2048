using System.Collections.Generic;
using Zenject;
using System;
using Random = UnityEngine.Random;

public class ControllerSections : ControllerObject<Section> {
    public override IReadOnlyList<Section> Objects => _sections;

    protected override bool CanSpawn => _sections.Count < GameplayConfig.NumberSpawnedSection;
    protected override int CountSpawn => GameplayConfig.NumberSpawnedSection - _sections.Count;

    private List<Section> _sections;
    private Player _player;

    public override void Initialize() {
        _sections = new List<Section>();
        base.Initialize();
    }

    [Inject]
    private void Construct(SectionPool memoryPool, Player player) {
        MemoryPool = memoryPool;
        _player = player;
    }

    protected override void Subscribe() {
        base.Subscribe();
        SignalBus.Subscribe<AddedSectionSignal>(OnAddedSection);
    }

    protected override void Unsubscribe() {
        base.Subscribe();
        SignalBus.Unsubscribe<AddedSectionSignal>(OnAddedSection);
    }

    protected override void RemoveFromCollection(Section removedObject) {
        _sections.Remove(removedObject);
    }

    protected override void AddInCollection(Section addedObject) {
        _sections.Add(addedObject);
    }

    protected override void OnReleasedObject(ReleasedObjectSignal<Section> signal) {
        MemoryPool.Despawn(signal.ReleasedObject);
        SpawnObject();
    }

    protected override void InitializeComponent(Section gameObject) {
        base.InitializeComponent(gameObject);
        int level = Random.Range(0, Math.Max(0, _player.Level - 3));

        gameObject.SetLevel(level);
    }

    private void OnAddedSection(AddedSectionSignal signal) {
        RemoveFromCollection(signal.SectionAdded);
        SpawnObject();
    }
}