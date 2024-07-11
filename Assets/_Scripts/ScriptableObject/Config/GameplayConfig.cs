using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
    [field: SerializeField] public Vector3 PointSpawn {  get; private set; }
    [field: SerializeField, Range(0, 50)] public int NumberSpawnedEnemy { get; private set; }
    [field: SerializeField, Range(1, 150)] public int NumberSpawnedSection { get; private set; }
    [SerializeField] private string nicknames;

    [SerializeField, Range(0, 49.5f)] private float radiusSpawn;

    public string Nicknames => nicknames;
    public float MinimalPositionSpawnObject => -radiusSpawn;
    public float MaximalPositionSpawnObject => radiusSpawn;
}