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
        if (CheckHost(signal)) {
            ProcessMerge(signal);
        } 
        else {
            DeleteElementFromCollection(signal);
        }
    }

    private void ProcessMerge(MergedSectionSignal signal) {
        RotateBody(signal);
        MoveBody(signal);
        signal.Time -= Time.deltaTime;
        if (CheckSatisfiesDistanceAndTime(signal)) {
            OnCopletedMethod(signal);
        }
    }

    private void RotateBody(MergedSectionSignal signal) {
        signal.DeleteSection.transform.LookAt(signal.UpgradeSection.PositionBack);
    }

    private void MoveBody(MergedSectionSignal signal) {
        signal.DeleteSection.transform.position += GetDirectionMove(signal) * CalculateSpeed(signal) * Time.deltaTime;
    }

    private void OnCopletedMethod(MergedSectionSignal signal) {
        DeleteElementFromCollection(signal);
        signal.UpgradeSection.Upgrade();
        ReleasedSection(signal.DeleteSection);
        signal.StorageSection.DeleteSectionFromCollection(signal.DeleteSection);
    }

    private static bool CheckHost(MergedSectionSignal signal) {
        return signal.DeleteSection.CheckOwnerSection(signal.StorageSection)
                    && signal.UpgradeSection.CheckOwnerSection(signal.StorageSection);
    }
    private bool CheckSatisfiesDistanceAndTime(MergedSectionSignal signal) {
        return GetLengthBeetwenSection(signal) < 1f && signal.Time <= 0.1f;
    }

    private void DeleteElementFromCollection(MergedSectionSignal signal) {
        _mergedSectionSignals.Remove(signal);
    }

    private void AddElementInCollection(MergedSectionSignal signal) {
        _mergedSectionSignals.Add(signal);
    }

    private float CalculateSpeed(MergedSectionSignal signal) {
        return GetLengthBeetwenSection(signal) / signal.Time;
    }

    private float GetLengthBeetwenSection(MergedSectionSignal signal) {
        return (signal.UpgradeSection.Position - signal.DeleteSection.Position).magnitude;
    }

    private Vector3 GetDirectionMove(MergedSectionSignal signal) {
        return (signal.UpgradeSection.Position - signal.DeleteSection.Position).normalized;
    }

    private void ReleasedSection(Section section) {
        _signalBus.Fire(new ReleasedObjectSignal<Section>(section, true));
    }

    public void Dispose() {
        Unsubscribe();
    }
}