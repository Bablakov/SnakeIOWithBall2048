using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject {
    [field: SerializeField] public SectionConfig Section { get; private set; }
    [field: SerializeField] public SnakeConfig Snake { get; private set; }
    [field: SerializeField] public GameplayConfig Gameplay { get; private set; }
    [field: SerializeField] public SoundConfig Sound { get; private set; }
}