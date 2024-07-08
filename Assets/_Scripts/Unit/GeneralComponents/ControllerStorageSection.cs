using System;
using Zenject;
using System.Collections.Generic;

public class ControllerStorageSection {
    public IReadOnlyCollection<Section> _sections => _storageSection.Sections;
    public event Action AddedSection;

    private StorageSection _storageSection;
    private CollisionHandler _collisionHandler;
    private SignalBus _signalBus;
    private Section _head;

    public ControllerStorageSection(SignalBus signalBus, Section head, StorageSection storageSection, CollisionHandler collisionHandler) {
        _head = head;
        _signalBus = signalBus;
        _storageSection = storageSection;
        _collisionHandler = collisionHandler;
        Subscribe();
    }

    private void Subscribe() {
        _collisionHandler.TouchedSection += OnTouckedSection;
        _storageSection.MergedSection += OnMergedSection;
    }

    private void Unsubscribe() {
        _collisionHandler.TouchedSection -= OnTouckedSection;
        _storageSection.MergedSection -= OnMergedSection;
    }

    private void OnTouckedSection(Section section) {
        if (IsLevelSitualbe(section) && IsNotItemInCollection(section)) {
            _signalBus.Fire(new AddedSectionSignal(section));
            _storageSection.Add(section);
            AddedSection?.Invoke();
        }
    }

    private void OnMergedSection(Section upgradeSection, Section deleteSection) {
        _signalBus.Fire(new MergedSectionSignal(_storageSection, upgradeSection, deleteSection));
    }

    private bool IsNotItemInCollection(Section section) {
        return !_storageSection.Contains(section);
    }

    private bool IsLevelSitualbe(Section section) {
        return _head.Level >= section.Level;
    }

    public void Dispose(){
        Unsubscribe();
    }
}