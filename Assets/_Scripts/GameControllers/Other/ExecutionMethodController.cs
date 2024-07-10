using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExecutionMethodController : ITickable, IDisposable {
    private List<CalledDelayedMethodSignal> _calledDelayedMethods;
    private SignalBus _signalBus;

    [Inject] 
    public ExecutionMethodController(SignalBus signalBus) {
        _signalBus = signalBus;
        _calledDelayedMethods = new List<CalledDelayedMethodSignal>();
        Subscribe();
    }

    public void Tick() {
        var methods = new List<CalledDelayedMethodSignal>(_calledDelayedMethods);
        foreach (var method in methods) {
            if (method.Time < 0) {
                method.Action();
                RemoveCalledMethod(method);

            }
            else {
                method.Time -= Time.deltaTime;
            }
        }
    }

    public void Dispose() {
        Unsubscribe();
    }

    private void Subscribe() {
        _signalBus.Subscribe<CalledDelayedMethodSignal>(AddCalledMethod);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<CalledDelayedMethodSignal>(AddCalledMethod);
    }

    private void AddCalledMethod(CalledDelayedMethodSignal signal) {
        _calledDelayedMethods.Add(signal);
    }

    private void RemoveCalledMethod(CalledDelayedMethodSignal signal) {
        _calledDelayedMethods.Remove(signal);
    }
}