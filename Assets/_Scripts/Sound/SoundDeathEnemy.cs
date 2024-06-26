public class SoundDeathEnemy : SoundPlayByEventPlayer {
    protected override void Subscribe() {
        Player.KilledEnemy += PlaySound;
    }

    protected override void Unsubscribe() {
        Player.KilledEnemy -= PlaySound;
    }
}