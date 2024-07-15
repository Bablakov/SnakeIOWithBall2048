using Zenject;

public abstract class SoundPlayByEventPlayer : SoundPlayByEvent {
    protected Player Player;

    [Inject]
    private void Construct(Player player) {
        Player = player;
    }
}