using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonStandart : BaseButton {
    protected Image Image;
    
    private Button _button;
    private UnityAction _buttonClickEvent;

    public override void Initialize() {
        GetComponents();
        AddMethodInEventClick(ClickOnButton);
    }

    protected virtual void GetComponents() {
        _button = GetComponent<Button>();
        Image = GetComponent<Image>();
    }

    protected override void AddMethodInEventClick(UnityAction action) {
        _buttonClickEvent += action;
    }

    protected override void AddEventOnButton() {
        _button.onClick.AddListener(_buttonClickEvent);
    }

    protected override void ClickOnButton() {
    }
}