using UnityEngine;

public class BootstrapMenu : MonoBehaviour {
    [SerializeField] private UIController uiController;

    private void Awake() {
        uiController.Initialize();
    }
}