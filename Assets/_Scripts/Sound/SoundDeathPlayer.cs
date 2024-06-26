public class SoundDeathPlayer : SoundPlayByEventPlayer {
    protected override void Subscribe() {
        Player.DiedPlayer += PlaySound;
    }

    protected override void Unsubscribe() {
        Player.DiedPlayer -= PlaySound;
    }
}