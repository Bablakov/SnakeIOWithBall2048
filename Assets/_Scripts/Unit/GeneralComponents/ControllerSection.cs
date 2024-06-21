using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;
using System;

public class ControllerSection {
    public IReadOnlyCollection<Section> Sections => _sections;

    private CollisionHandler _collisionHandler;
    private LinkedList<Section> _sections;
    private SignalBus _signalBus;
    private Section _head;
    private Unit _owner;

    public ControllerSection(CollisionHandler collisionHandler, SignalBus signalBus, Section head) {
        _collisionHandler = collisionHandler;
        _signalBus = signalBus;
        _head = head;
        _head.SetNewControllerSection(this);
        _sections = new LinkedList<Section>();
        _sections.AddFirst(head);
        Subscribe();
    }
    
    public void AddElement(Section section) {
        OnAddedSeciton(section);
    }

    public void DeleteSectionFromCollection(Section section) {
        var nodeSection = _sections.Find(section);

        if (nodeSection != null) {
            var next = nodeSection.Next;
            _sections.Remove(section);
            if (next != null) {
                DeleteSectionFromCollection(next);
            }
        }
        
    }

    public void FreeCollection() {
        if (_sections.First.Next != null) {
            DeleteSectionFromCollection(_sections.First.Next);
        }
    }

    private void DeleteSectionFromCollection(LinkedListNode<Section> node) {
        var next = node.Next;
        _sections.Remove(node);
        if (next != null) {
            DeleteSectionFromCollection(next);
        }
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
        }
    }

    private bool IsNotItemInCollection(Section section) {
        return !_sections.Contains(section);
    }

    private bool IsLevelSitualbe(Section section) {
        return _sections.First.Value.Level >= section.Level;
    }

    private void AddElementInCollection(Section section) {
        _signalBus.Fire(new AddedSectionSignal(section));
        
        LinkedListNode<Section> currentSection = _sections.First;
        bool combine = false;

        while (true) {
            if (currentSection.Value.Level == section.Level) {
                combine = true;
                break;
            }
            else if (currentSection.Value.Level < section.Level) {
                currentSection = currentSection.Previous;
                break;
            }
            else if (currentSection.Next == null) {
                break;
            }

            if (currentSection.Next.Value.Level >= section.Level) {
                currentSection = currentSection.Next;
            }
            else {
                break;
            } 
        }

        if (combine) {
            section.SetNewControllerSection(this);
            MergeSections(currentSection, section);
        }
        else {
            section.SetNewControllerSection(this);
            _sections.AddAfter(currentSection, section);
        }
    }

    private void MergeSections(LinkedListNode<Section> sectionCombine, Section section) {
        sectionCombine.Value.Upgrade();
        _signalBus.Fire(new ReleasedSectionSignal(section));

        if (sectionCombine != _sections.First && sectionCombine.Previous.Value.Level == sectionCombine.Value.Level) {
            MergeSections(sectionCombine.Previous);
        }
    }

    private void MergeSections(LinkedListNode<Section> sectionCombine) {
        sectionCombine.Value.Upgrade();
        var beetwen = sectionCombine.Next;
        _sections.Remove(beetwen);
        _signalBus.Fire(new ReleasedSectionSignal(beetwen.Value));

        if (sectionCombine != _sections.First && sectionCombine.Previous.Value.Level == sectionCombine.Value.Level) {
            MergeSections(sectionCombine.Previous);
        }
    }
}