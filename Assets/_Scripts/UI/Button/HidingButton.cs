using UnityEngine.UI;

public class HidingButton : StandartButton {
    public virtual void Hide() {
        Image.enabled = false;
    }

    public virtual void Show() {
        Image.enabled = true;
    }
}