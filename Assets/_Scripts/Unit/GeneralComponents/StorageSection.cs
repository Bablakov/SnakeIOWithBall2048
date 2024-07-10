using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StorageSection {
    public event Action<Section, Section> MergedSection;

    public IReadOnlyCollection<Section> Sections => _sections;

    private LinkedList<Section> _sections;
    private Section _head;

    private float _currentTime = 1f;
    private float _timeUpdate = 1f;
    private bool _invulnerability = false;

    public StorageSection(Section firstSection) {
        CreateComponent();
        _sections.AddFirst(firstSection);
        _head = firstSection;
        SetController(firstSection);
    }

    public void FreeCollection() {
        if (_sections.First.Next != null) {
            DeleteElementFromCollection(_sections.First.Next);
        }
    }

    public void Delete(Section section) {
        _sections.Remove(section);
    }

    public void DeleteSectionFromCollection(Section section) {
        section.SetVulnerability();
        DeleteController(section);
        _sections.Remove(section);
    }

    public bool Contains(Section section) {
        return _sections.Contains(section);
    }

    public void Add(Section element) {
        if (_invulnerability) {
            element.SetInvulnerability();
        }

        var previousElment = FindPrecedingElement(element);

        SetController(element);
        _sections.AddAfter(previousElment, element);
    }

    public void Update() {
        if (_currentTime < 0) {
            CheckMergeElements();
            _currentTime = _timeUpdate;
        } else {
            _currentTime -= Time.deltaTime;
        }
    }

    public void MakeSectionsInvulnerable() {
        _invulnerability = true;
        var sections = new List<Section>(_sections);
        foreach (var section in sections) {
            section.SetInvulnerability();
        }
    }

    public void MakeSectionsVulnerable() {
        _invulnerability = false;
        var sections = new List<Section>(_sections);
        foreach (var section in sections) {
            section.SetVulnerability();
        }
    }

    private void CheckMergeElements() {
        var beetwen = _sections.GroupBy(element => element.Level)
                        .Where(collection => collection.Count() > 1)
                        .OrderByDescending(element => element.Key);
        if (beetwen.Count() > 0) {
            foreach (var beet in beetwen)
                MergeElement(_sections.Find(beet.First()));
        }
    }

    private void CreateComponent() {
        _sections = new LinkedList<Section>();
    }

    private void DeleteElementFromCollection(LinkedListNode<Section> node) {
        var nextNode = node.Next;
        DeleteController(node.Value);
        if (nextNode != null) {
            DeleteElementFromCollection(nextNode);
        }
    }

    private LinkedListNode<Section> FindPrecedingElement(Section element) {
        var currentElement = _sections.First;
       
        foreach (var section in _sections) {
            section.SetSequence(null);
        }

        while (true) {
            if (currentElement.Value.Level == element.Level) {
                break;
            } else if (currentElement.Value.Level < element.Level) {
                currentElement = currentElement.Previous;
                break;
            } else if (currentElement.Next == null) {
                break;
            }

            if (currentElement.Next.Value.Level >= element.Level) {
                currentElement.Value.SetSequence(currentElement.Next.Value.PlayAnimation);
                currentElement = currentElement.Next;
            } else {
                break;
            }
        }

        _sections.First.Value.PlayAnimation();

        return currentElement;
    }

    private void MergeElement(LinkedListNode<Section> elementCombine) {
        var beetwen = elementCombine.Next;
        AnimationMerge(elementCombine.Value, beetwen.Value);
    }

    private void AnimationMerge(Section upgradeElement, Section deleteElement) {
        MergedSection?.Invoke(upgradeElement, deleteElement);
    }

    private void SetController(Section element) {
        element.SetNewControllerSection(this);
    }

    private void DeleteController(Section element) {
        element.SetNewControllerSection(null);
    }
}