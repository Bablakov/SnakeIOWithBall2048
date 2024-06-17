using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlledElement : MonoBehaviour {
    private ParametrsPlayer _parametrsPlayer;
    private Rotation _rotation;
    private Movement _movement;
    private Player _player;

    private List<Section> bodyElements {
        get {
            return _player.bodyElements;
        }

        set { _player.bodyElements = value; }
    }

    public void Initialize(ParametrsPlayer parametrsPlayer, Player player) {
        _parametrsPlayer = parametrsPlayer;
        _player = player;
        CreateComponents();
    }

    private void Update() {
        Rotate();
        Move();
    }

    private void CreateComponents() {
        _movement = new Movement(transform);
        _rotation = new Rotation(transform);
    }

    private void Rotate() {
        _rotation.Rotate(_parametrsPlayer.DirectionMovement);
    }

    private void Move() {
        _movement.Move(_parametrsPlayer.Speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null && other.transform.parent.TryGetComponent(out Section section)) {
            if (!bodyElements.Contains(section) && bodyElements[0].Level >= section.Level) {
                bodyElements.Add(section);
                CheckElementsDublicate();
                bodyElements = bodyElements
                    .OrderByDescending(section => section.Level)
                    .ToList();
            }
        }
    }

    private void CheckElementsDublicate() {
        var collection = bodyElements.GroupBy(section => section.Level);
        if (collection.Any(element => element.Count() > 1)) {
            var beetwen = collection
                .Where(element => element.Count() > 1)
                .ToList();
            foreach (var element in beetwen) {
                element.First().Upgrade();
                for (int i = 1; i < element.Count(); i++) {
                    bodyElements.Remove(element.ElementAt(i));
                    Destroy(element.ElementAt(i).transform.gameObject);
                }
            }
            CheckElementsDublicate();
        }
    }
}