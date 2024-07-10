using UnityEngine;
using Zenject;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;

    private void Awake() {
        player.Initialize("Вы");
    }
}