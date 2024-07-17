using TMPro;
using UnityEngine;

public class ResultPanel : AnimatedPanel {
    [SerializeField] private TextMeshProUGUI viewBestPointSection;
    [SerializeField] private TextMeshProUGUI viewBestNumberKilled;
    [SerializeField] private TextMeshProUGUI viewCurrentPointSection;
    [SerializeField] private TextMeshProUGUI viewCurrentNumberKilled;
    [SerializeField] private ButtonGoMenu buttonGoMenu;

    public void Initialize() {
        buttonGoMenu.Initialize();
    }

    public void Enable(string bestPointSection, string bestNumberKilled, string currentPointSection, string currentNumberKilled) {
        gameObject.SetActive(true);
        viewBestPointSection.text = bestPointSection;
        viewBestNumberKilled.text = bestNumberKilled;
        viewCurrentPointSection.text = currentPointSection;
        viewCurrentNumberKilled.text = currentNumberKilled;
        EnableAnimation();
    }

    public void Disable() {
        gameObject.SetActive(false);
        DisableAnimation();
    }
}