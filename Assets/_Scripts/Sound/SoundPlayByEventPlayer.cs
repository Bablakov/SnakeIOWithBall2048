using UnityEngine;
using Zenject;

public abstract class SoundPlayByEventPlayer : SoundPlayByEvent {
    protected Player Player;

    public override void Initialize(AudioClip clip) {
        base.Initialize(clip);
        Subscribe();
    }

    [Inject]
    private void Construct(Player player) {
        Player = player;
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}