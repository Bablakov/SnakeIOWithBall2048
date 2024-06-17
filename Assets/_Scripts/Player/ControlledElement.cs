using System.Collections.Generic;
using UnityEngine;

public class ControlledElement : MonoBehaviour {
    private float _speed = 1f;
    private Rotation _rotation;
    private Movement _movement;
    private InputGame _inputGame;
    private ControlledElement _controlledElement;
    private Player _player;

    private List<Section> bodyElements => _player.bodyElements;

    public void Initialize(InputGame inputGame, float speed, Player player) {
        _inputGame = inputGame;
        _speed = speed;
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
        _rotation.Rotate(_inputGame.GetDirectionMovememt());
    }

    private void Move() {
        _movement.Move(_speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null && other.transform.parent.TryGetComponent(out Section section)) {
            if (!bodyElements.Contains(section) && bodyElements[0].Level > section.Level) {
                bodyElements.Add(section);
                //bodyElements = bodyElements.OrderByDescending(section => section.Level).ToList();
            }
        }
    }
}