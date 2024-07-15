using UnityEngine;
using Zenject;

public abstract class UIController : MonoBehaviour, IInitializable {
    public virtual void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    protected abstract void GetComponents();

    protected abstract void InitializeComponents();
}