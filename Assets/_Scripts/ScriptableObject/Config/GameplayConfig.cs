using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
    [field: SerializeField] public Vector3 PointSpawn {  get; private set; }
    [field: SerializeField, Range(1, 50)] public int NumberSpawnedEnemy { get; private set; }
    [field: SerializeField, Range(1, 150)] public int NumberSpawnedSection { get; private set; }

    [SerializeField, Range(0, 100f)] private float radiusSpawn;

    public float MinimalPositionSpawnObject => -radiusSpawn;
    public float MaximalPositionSpawnObject => radiusSpawn;
}