using UnityEngine;

[CreateAssetMenu(fileName = "SnakeConfig", menuName = "Configs/SnakeCofig")]
public class SnakeConfig : ScriptableObject {
    [field: SerializeField, Range(0.1f, 100f)] public float Speed { get; private set; }
    [field: SerializeField, Range(0.1f, 2f)] public float DistanceBeetwen { get; private set; }
}
