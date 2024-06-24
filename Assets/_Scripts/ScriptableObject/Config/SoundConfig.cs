using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig")]
public class SoundConfig : ScriptableObject {
    [field: SerializeField] public AudioClip ClickButton { get; private set; }
    [field: SerializeField] public AudioClip PickUpSection { get; private set; }
    [field: SerializeField] public AudioClip DeathPlayer { get; private set; }
    [field: SerializeField] public AudioClip DeathEnemy { get; private set; }
    [field: SerializeField] public AudioClip Background { get; private set; }
}