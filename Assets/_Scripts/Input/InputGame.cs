using System;
using UnityEngine;

public interface InputGame {
    public abstract event Action SpeededUp;
    public abstract event Action SpeededDown;

    public abstract Vector3 GetDirectionMovememt();
}