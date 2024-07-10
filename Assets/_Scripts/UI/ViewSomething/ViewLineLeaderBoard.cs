using UnityEngine;
using TMPro;

public class ViewLineLeaderBoard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI position;
    [SerializeField] private TextMeshProUGUI nickname;
    [SerializeField] private TextMeshProUGUI numberPoints;

    public void SetValue(string position, string nickname, string numberPoints) {
        this.position.text = position;
        this.nickname.text = nickname;
        this.numberPoints.text = numberPoints;
    }
}