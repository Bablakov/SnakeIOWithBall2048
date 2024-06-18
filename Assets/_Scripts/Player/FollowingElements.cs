using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class FollowingElements : MonoBehaviour {
    private ParametsSnake _parametrsPlayer;
    private SignalBus _signalBus;
    private Player _player;
    private SectionPool _sectionPool;

    private List<Section> _bodyElements {
        get {
            return _player.bodyElements;
        }
        set { _player.bodyElements = value; }
    }

    [Inject]
    public void Construct(SignalBus signalBus, SectionPool sectionPool) {
        _signalBus = signalBus;
        _sectionPool = sectionPool;
        Subscribe();
    }


    public void Initialize(ParametsSnake parametrsPlayer, Player player) {
        _parametrsPlayer = parametrsPlayer;
        _player = player;
    }

    private void Update() {
        MoveAndRotateSections();
    }

    private void Subscribe() {
        _signalBus.Subscribe<AddedSectionSignal>(OnAddedSeciton);
    }

    private void OnAddedSeciton(AddedSectionSignal signal) {
        if (!_bodyElements.Contains(signal.SectionAdded) && _bodyElements[0].Level >= signal.SectionAdded.Level) {
            _bodyElements.Add(signal.SectionAdded);
            CheckElementsDublicate();
            _bodyElements = _bodyElements
                .OrderByDescending(section => section.Level)
                .ToList();
        }
    }

    private void CheckElementsDublicate() {
        var collection = _bodyElements.GroupBy(section => section.Level);
        if (collection.Any(element => element.Count() > 1)) {
            var beetwen = collection
                .Where(element => element.Count() > 1)
                .ToList();
            foreach (var element in beetwen) {
                element.First().Upgrade();
                for (int i = 1; i < element.Count(); i++) {
                    _bodyElements.Remove(element.ElementAt(i));
                    _sectionPool.Despawn(element.ElementAt(i));
                }
            }
            CheckElementsDublicate();
        }
    }

    private void MoveAndRotateSections() {
        for (int i = 1; i < _bodyElements.Count; i++) {
            MoveAndRotateSection(i);
        }
    }

    private void MoveAndRotateSection(int index) {
        var backPartNextSection = _bodyElements[index - 1].PositionBack;
        var coeficientMovement = CalculateCoefficient(index);
        RotateBody(index, backPartNextSection);
        MoveBody(index, coeficientMovement);
    }

    private float CalculateCoefficient(int index) {
        return (GetTotalSizeBeetwenCenterSection(index) + _parametrsPlayer.DistanceBeetwenSection) / GetLengthBeetwenSection(index);
    }

    private void RotateBody(int index, Vector3 backNext) {
        _bodyElements[index].transform.LookAt(backNext);
    }

    private void MoveBody(int index, float coefficient) {
        _bodyElements[index].transform.position += GetDirectionMove(index) * _parametrsPlayer.Speed / coefficient * Time.deltaTime;
    }

    private float GetLengthBeetwenSection(int index) {
        return (_bodyElements[index - 1].Position - _bodyElements[index].Position).magnitude;
    }

    private float GetTotalSizeBeetwenCenterSection(int index) {
        return _bodyElements[index].Width + _bodyElements[index - 1].Width;
    }

    private Vector3 GetDirectionMove(int index) {
        return (_bodyElements[index - 1].Position - _bodyElements[index].Position).normalized;
    }
}