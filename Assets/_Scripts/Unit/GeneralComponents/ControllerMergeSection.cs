using Zenject;
using System;
using UnityEngine;
using System.Collections.Generic;

public class ControllerMergeSection : IDisposable, ITickable {
    [SerializeField, Range(0.1f, 10f)] private float durationAnimation = 0.3f;
    private SignalBus _signalBus;
    private List<MergedSectionSignal> _mergedSectionSignals;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
        _mergedSectionSignals = new List<MergedSectionSignal>();
        Subscribe();
    }

    public void Tick() {
        var beetwenCollection = _mergedSectionSignals.ToArray();
        foreach (var signal in beetwenCollection) {
            ProcessMergeSection(signal);
        }
    }

    private void Subscribe() {
        _signalBus.Subscribe<MergedSectionSignal>(OnMergedSection);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<MergedSectionSignal>(OnMergedSection);
    }

    private void OnMergedSection(MergedSectionSignal signal) {
        AddElementInCollection(signal);
    }

    private void ProcessMergeSection(MergedSectionSignal signal) {
        RotateBody(signal);
        MoveBody(signal);
        signal.Time -= Time.deltaTime;
        if (GetLengthBeetwenSection(signal) < 1f && signal.Time <= 0.1f) {
            OnCopletedMethod(signal);
        }
    }

    private void OnCopletedMethod(MergedSectionSignal signal) {
        signal.UpgradeSection.Upgrade();
        DeleteElementFromCollection(signal);
        _signalBus.Fire(new ReleasedObjectSignal<Section>(signal.DeleteSection, true));
        signal.StorageSection.CheckCombineElement(signal.UpgradeSection);
    }

    public void Dispose() {
        Unsubscribe();
    }

    private void AddElementInCollection(MergedSectionSignal signal) {
        _mergedSectionSignals.Add(signal);
    }

    private void DeleteElementFromCollection(MergedSectionSignal signal) {
        _mergedSectionSignals.Remove(signal);
    }

    private float CalculateSpeed(MergedSectionSignal signal) {
        return GetLengthBeetwenSection(signal) / signal.Time;
    }

    private void RotateBody(MergedSectionSignal signal) {
        signal.DeleteSection.transform.LookAt(signal.UpgradeSection.PositionBack);
    }

    private void MoveBody(MergedSectionSignal signal) {
        signal.DeleteSection.transform.position += GetDirectionMove(signal) * CalculateSpeed(signal) * Time.deltaTime;
    }

    private float GetLengthBeetwenSection(MergedSectionSignal signal) {
        return (signal.UpgradeSection.Position - signal.DeleteSection.Position).magnitude;
    }

    private Vector3 GetDirectionMove(MergedSectionSignal signal) {
        return (signal.UpgradeSection.Position - signal.DeleteSection.Position).normalized;
    }
}