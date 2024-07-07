using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StorageSection {
    public event Action<Section, Section> MergedSection;

    public IReadOnlyCollection<Section> Sections => _sections;

    private LinkedList<Section> _sections;

    public StorageSection(Section firstSection) {
        CreateComponent();
        _sections.AddFirst(firstSection);
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

    public bool Contains(Section section) {
        return _sections.Contains(section);
    }

    public void Add(Section element) {
        var previousElment = FindPrecedingElement(element, out bool combine);

        SetController(element);
        if (combine) {
            MergeElement(previousElment, element);
        } else {
            _sections.AddAfter(previousElment, element);
        }
    }

    public void CheckCombineElement(Section section) {
        var sect = _sections.Find(section);
        while (true) {
            if (sect == null) {
                break;
            }

            if (sect.Previous == null) {
                break;
            }
            else if (sect.Previous.Value.Level == sect.Value.Level) {
                AnimationMerge(sect.Previous.Value, section);
                break;
            }

            sect = sect.Previous;
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

    private LinkedListNode<Section> FindPrecedingElement(Section element, out bool combine) {
        var currentElement = _sections.First;
        combine = false;
       
        foreach (var section in _sections) {
            section.SetSequence(null);
        }

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
                currentElement.Value.SetSequence(currentElement.Next.Value.PlayAnimation);
                currentElement = currentElement.Next;
            } else {
                break;
            }
        }

        _sections.First.Value.PlayAnimation();

        return currentElement;
    }

    private void MergeElement(LinkedListNode<Section> elementCombine, Section section) {
        DeleteController(section);
        AnimationMerge(elementCombine.Value, section);

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }

    private void MergeElement(LinkedListNode<Section> elementCombine) {
        var beetwen = elementCombine.Next;
        var beetwenSignal = elementCombine.Next.Value;
        DeleteController(beetwenSignal);
        AnimationMerge(elementCombine.Value, beetwen.Value);

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }

    private void AnimationMerge(Section upgradeElement, Section deleteElement) {
        _sections.Remove(deleteElement);
        MergedSection?.Invoke(upgradeElement, deleteElement);
    }

    private void SetController(Section element) {
        element.SetNewControllerSection(this);
    }

    private void DeleteController(Section element) {
        element.SetNewControllerSection(null);
    }
}