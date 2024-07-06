using Zenject;
using UnityEngine;
using DG.Tweening;

public class ControllerMergeSection {
    private SignalBus _signalBus;
    private float durationAnimation = 0.5f;

    [Inject]
    public ControllerMergeSection(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<MergedSectionSignal>(OnMergedSection);
    }

    private void Unsubscribe() {
        _signalBus.Subscribe<MergedSectionSignal>(OnMergedSection);
    }

    private void OnMergedSection(MergedSectionSignal signal) {
        signal.DeleteSection.transform.DOMove(signal.UpgradeSection.transform.position, durationAnimation)
            .OnComplete(() =>
            Debug.Log("MergedSections"));
    }
}