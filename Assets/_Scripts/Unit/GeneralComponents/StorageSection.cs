using System;
using System.Collections.Generic;

public class StorageSection {
    public event Action<Section, bool> DeletedSection;
    
    public IReadOnlyCollection<Section> Sections => _sections;

    private LinkedList<Section> _sections;

    public StorageSection(Section firstSection) {
        CreateComponent();
        _sections.AddFirst(firstSection);
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
        while (true) {
            currentElement.Value.PlayAnimation();
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

    private void MergeElement(LinkedListNode<Section> elementCombine, Section section) {
        DeleteElement(section, true);
        DeleteController(section);

        elementCombine.Value.Upgrade();

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }

    private void MergeElement(LinkedListNode<Section> elementCombine) {
        var beetwen = elementCombine.Next;
        var beetwenSignal = elementCombine.Next.Value;
        DeleteElement(beetwen, true);
        DeleteController(beetwenSignal);
        
        elementCombine.Value.Upgrade();

        if (elementCombine != _sections.First && elementCombine.Previous.Value.Level == elementCombine.Value.Level) {
            MergeElement(elementCombine.Previous);
        }
    }

    private void DeleteElement(Section element, bool needSpawnRepeat) {
        _sections.Remove(element);
        DeletedSection?.Invoke(element, needSpawnRepeat);
    }

    private void DeleteElement(LinkedListNode<Section> element, bool needSpawnRepeat) {
        _sections.Remove(element);
        DeletedSection?.Invoke(element.Value, needSpawnRepeat);
    }

    private void SetController(Section element) {
        element.SetNewControllerSection(this);
    }

    private void DeleteController(Section element) {
        element.SetNewControllerSection(null);
    }
}