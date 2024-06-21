using Zenject;

public class EnemyPool : MemoryPool<Enemy> {
    protected override void OnSpawned(Enemy item) {
        item.gameObject.SetActive(true);
    }

    protected override void OnDespawned(Enemy item) {
        item.gameObject.SetActive(false);
    }
}