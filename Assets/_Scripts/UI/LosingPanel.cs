using TMPro;
using UnityEngine;

public class LosingPanel : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI viewBestPointSection;
    [SerializeField] private TextMeshProUGUI viewBestNumberKilled;
    [SerializeField] private TextMeshProUGUI viewCurrentPointSection;
    [SerializeField] private TextMeshProUGUI viewCurrentNumberKilled;
    [SerializeField] private ButtonGoMenu buttonGoMenu;
    [SerializeField] private ButtonRevivalPlayer buttonRevivalPlayer;

    public void Initialize() {
        buttonGoMenu.Initialize();
        buttonRevivalPlayer.Initialize();
        buttonRevivalPlayer.SetValue(this);
    }

    public void Enable(string bestPointSection, string bestNumberKilled, string currentPointSection, string currentNumberKilled) {
        gameObject.SetActive(true);
        viewBestPointSection.text = bestPointSection;
        viewBestNumberKilled.text = bestNumberKilled;
        viewCurrentPointSection.text = currentPointSection;
        viewCurrentNumberKilled.text = currentNumberKilled;
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}