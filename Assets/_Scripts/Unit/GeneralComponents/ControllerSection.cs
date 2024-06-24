using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerSection {
    public IReadOnlyCollection<Section> Sections => _sections;
    public event Action<Section> AddedSection;

    private CollisionHandler _collisionHandler;
    private LinkedList<Section> _sections;
    private SignalBus _signalBus;
    private Section _head;
    private Unit _owner;

    public ControllerSection(CollisionHandler collisionHandler, SignalBus signalBus, Section head, Unit owner) {
        _sections = new LinkedList<Section>();

        _collisionHandler = collisionHandler;
        _signalBus = signalBus;
        _head = head;
        _owner = owner;

        _sections.AddFirst(head);

        SetController(_head);
        Subscribe();
    }
    
    public void AddElement(Section section) {
        AddElementInCollection(section);
        AddedSection?.Invoke(section);
    }

    public void FreeCollection() {
        if (_sections.First.Next != null) {
            DeleteElementFromCollection(_sections.First.Next);
        }
    }

    public void DeleteElementFromCollection(Section section) {
        _sections.Remove(section);
    }

    private void Subscribe() {
       _collisionHandler.AddedSection += OnAddedSeciton;
    }

    private void Unsubscribe() {
        _collisionHandler.AddedSection -= OnAddedSeciton;
    }

    private void OnAddedSeciton(Section element) {
        if (IsNotItemInCollection(element) && IsLevelSitualbe(element)) {
            AddElementInCollection(element);
            AddedSection?.Invoke(element);
            if (_owner is Player) {
                _signalBus.Fire(new PutOnSectionSignal());
            }
        }
    }

    private bool IsNotItemInCollection(Section section) {
        return !_sections.Contains(section);
    }

    private bool IsLevelSitualbe(Section section) {
        return _sections.First.Value.Level >= section.Level;
    }

    private void AddElementInCollection(Section element) {
        _signalBus.Fire(new AddedSectionSignal(element));
        var previousElment = FindPrecedingElement(element, out bool combine);

        SetController(element);
        if (combine) {
            MergeElement(previousElment, element);
        } else {
            _sections.AddAfter(previousElment, element);
        }
    }

    private void DeleteElementFromCollection(LinkedListNode<Section> node) {
        var nextNode = node.Next;
        var beetwen = node.Value;
        _sections.Remove(beetwen);
        _signalBus.Fire(new ReleasedObjectSignal<Section>(beetwen));
        DeleteController(beetwen);
        if (nextNode != null) {
            DeleteElementFromCollection(nextNode);
        }
    }

    private LinkedListNode<Section> FindPrecedingElement(Section element, out bool combine) {
        var currentElement = _sections.First;
        combine = false;
        while (true) {
            if (currentElement.Value.Level == element.Level) {
                combine = true;
                break;
            } else if (currentElement.Value.Level < element.Level) {
                currentElement = currentElement.Previous;
                break;
            } else if (currentElement.Next == null) {
                break;
            }

            if (currentElement.Next.Value.Level >= element.Level) {
                currentElement = currentElement.Next;
            } else {
                break;
            }
        }

        return currentElement;
    }

    private void SetController(Section element) {
        element.SetNewControllerSection(this);
    }

    private void DeleteController(Section element) {
        element.SetNewControllerSection(null);
    }

    private void MergeElement(LinkedListNode<Section> elementCombine, Section section) {
        _signalBus.Fire(new ReleasedObjectSignal<Section>(section, true));
        DeleteController(section);

        elementCombine.Value.Upgrade();

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }

    private void MergeElement(LinkedListNode<Section> elementCombine) {
        var beetwen = elementCombine.Next;
        var beetwenSignal = elementCombine.Next.Value;
        _sections.Remove(beetwen);
        DeleteController(beetwenSignal);
        
        elementCombine.Value.Upgrade();
        _signalBus.Fire(new ReleasedObjectSignal<Section>(beetwenSignal, true));

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }
}