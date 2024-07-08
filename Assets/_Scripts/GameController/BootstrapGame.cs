using UnityEngine;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;

    private void Awake() {
        player.Initialize("Вы");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("------------------------------------------");
        }
    }
}