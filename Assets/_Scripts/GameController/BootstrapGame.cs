using UnityEngine;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;
    [SerializeField] private ControllerFreeSectionOnScene _controllerFreeSection;

    private void Awake() {
        player.Initialize();
        _controllerFreeSection.Initialize();
    }
}