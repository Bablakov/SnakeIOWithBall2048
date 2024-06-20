using System.Collections.Generic;
using System.Linq;
using Zenject;

public class ControllerSection {
    public IReadOnlyList<Section> Sections => _sections;

    private CollisionHandler _collisionHandler;
    private List<Section> _sections;
    private SignalBus _signalBus;
    private Section _head;
    
    public ControllerSection(CollisionHandler collisionHandler, SignalBus signalBus, Section head) {
        _collisionHandler = collisionHandler;
        _signalBus = signalBus;
        _head = head;
        _sections = new List<Section>() { head };
        Subscribe();
    }

    private void Subscribe() {
       _collisionHandler.AddedSection += OnAddedSeciton;
    }

    private void Unsubscribe() {
        _collisionHandler.AddedSection -= OnAddedSeciton;
    }

    private void OnAddedSeciton(Section section) {
        if (IsNotItemInCollection(section) && IsLevelSitualbe(section)) {
            AddElementInCollection(section);
            CheckElementsDublicate();
            SortCollection();
        }
    }

    private bool IsNotItemInCollection(Section section) {
        return !_sections.Contains(section);
    }

    private bool IsLevelSitualbe(Section section) {
        return _sections[0].Level >= section.Level;
    }

    private void AddElementInCollection(Section section) {
        _signalBus.Fire(new AddedSectionSignal(section));
        _sections.Add(section);
    }

    private void CheckElementsDublicate() {
        var collection = GroupCollection();

        if (AreIndenticalItemsInCollection(collection)) {
            var beetwen = SelectionIdenticalItems(collection);
            foreach (var collectionIndeticalItems in beetwen) {
                MergeSection(collectionIndeticalItems);
            }
            CheckElementsDublicate();
        }
    }

    private void SortCollection() {
        _sections = _sections
                        .OrderByDescending(section => section.Level)
                        .ToList();
    }

    private IEnumerable<IGrouping<int, Section>> GroupCollection() {
        return _sections
                    .GroupBy(section => section.Level);
    }

    private static bool AreIndenticalItemsInCollection(IEnumerable<IGrouping<int, Section>> collection) {
        return collection.Any(element => element.Count() > 1);
    }

    private static List<IGrouping<int, Section>> SelectionIdenticalItems(IEnumerable<IGrouping<int, Section>> collection) {
        return collection
            .Where(element => element.Count() > 1)
            .ToList();
    }

    private void MergeSection(IGrouping<int, Section> element) {
        UpgradeFirstSection(element);
        RemoveRemainingSection(element);
    }

    private static void UpgradeFirstSection(IGrouping<int, Section> element) {
        element.First().Upgrade();
    }

    private void RemoveRemainingSection(IGrouping<int, Section> element) {
        for (int i = 1; i < element.Count(); i++) {
            var releaseSection = element.ElementAt(i);
            _sections.Remove(releaseSection);
            _signalBus.Fire(new ReleasedSectionSignal(releaseSection));
        }
    }
}