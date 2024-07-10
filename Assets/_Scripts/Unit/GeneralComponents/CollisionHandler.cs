using UnityEngine;
using System;
using Zenject;

public class CollisionHandler : MonoBehaviour {
    public event Action<Section> TouchedSection;
    public event Action DiedEnemy;
    public event Action DiedMe;

    public int Level => _mineSectioin.Level;
    public string Nickname { get; private set; }

    private bool _isCollisionProcessed;
    private SignalBus _signalBus;
    private Section _mineSectioin;

    public void Initialize(string nickname) {
        Nickname = nickname;
        GetComponent();
    }

    [Inject]
    private void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void SetCollisionProcessed() {
        _isCollisionProcessed = true;
    }

    public void AddSection(Section section) {
        TouchedSection?.Invoke(section);
        DiedEnemy?.Invoke();
    }

    public void SetOff() {
        DiedMe?.Invoke();
    }

    private void GetComponent() {
        _mineSectioin = GetComponentInChildren<Section>();
    }

    private void RemoveCollisionProcessed() {
        _isCollisionProcessed = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!_isCollisionProcessed) {
            HandleCollision(other);
        } else {
            RemoveCollisionProcessed();
        }
    }

    private void HandleCollision(Collider other) {
        if (TryGetComponentSection(other, out Section section)) {
            HandleSection(other, section);
        }
    }

    private void HandleSection(Collider other, Section section) {
        if (TryGetComponentUnit(other, out CollisionHandler collisionHandler)) {
            if (!_mineSectioin.Invulnerability) {
                collisionHandler.SetCollisionProcessed();
                _signalBus.Fire(new ConflictedSignal(this, collisionHandler));
            }
        } else {
            TouchedSection?.Invoke(section);
        }
    }

    private bool TryGetComponentSection(Collider other, out Section section) {
        section = other.GetComponentInParent<Section>();
        if (section.Invulnerability)
            return false;
        return section != null;
    }

    private bool TryGetComponentUnit(Collider other, out CollisionHandler unit) {
        unit = other.GetComponentInParent<CollisionHandler>();
        return unit != null;
    }
}