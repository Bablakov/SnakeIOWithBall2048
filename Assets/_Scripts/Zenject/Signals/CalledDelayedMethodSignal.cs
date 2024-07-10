using System;

public class CalledDelayedMethodSignal {
    public float Time;
    public readonly Action Action;
    
    public CalledDelayedMethodSignal(float time, Action action) {
        Time = time;
        Action = action;
    }
}