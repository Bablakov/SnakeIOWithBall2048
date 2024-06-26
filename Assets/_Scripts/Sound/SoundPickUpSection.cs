public class SoundPickUpSection : SoundPlayByEventPlayer {
    protected override void Subscribe() {
        Player.PutOnSection += PlaySound;
    }

    protected override void Unsubscribe() {
        Player.PutOnSection -= PlaySound;
    }
}