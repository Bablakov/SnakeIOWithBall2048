using System;
using UnityEngine;
using Zenject;

public abstract class InputGame : MonoBehaviour {
    public event Action SpeededUp;
    public event Action SpeededDown;
    
    protected Vector3 DirectionMovement;

    public abstract Vector3 GetDirectionMovememt();

    protected void SpeedUp() {
        SpeededUp?.Invoke();
    }

    protected void SpeedDown() { 
        SpeededDown?.Invoke();
    }
}