using UnityEngine;

public class AnimationSpeedUp : MonoBehaviour {
    
    public void TurnOn() {
        gameObject.SetActive(true);
    }

    public void TurnOff() {
        gameObject.SetActive(false);
    }
}