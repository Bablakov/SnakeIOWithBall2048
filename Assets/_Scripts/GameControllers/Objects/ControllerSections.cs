using System.Collections.Generic;
using Zenject;

public class ControllerSections : ControllerObject<Section> {
    public override IReadOnlyList<Section> Objects => _sections;

    protected override bool CanSpawn => _sections.Count < GameplayConfig.NumberSpawnedSection;
    protected override int CountSpawn => GameplayConfig.NumberSpawnedSection - _sections.Count;

    private List<Section> _sections;

    public override void Initialize() {
        _sections = new List<Section>();
        base.Initialize();
    }

    [Inject]
    private void Construct(SectionPool memoryPool) {
        MemoryPool = memoryPool;
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

    private void OnAddedSection(AddedSectionSignal signal) {
        RemoveFromCollection(signal.SectionAdded);
        SpawnObject();
    }
}