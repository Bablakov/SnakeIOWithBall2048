using UnityEngine;
using DG.Tweening;

public class TestDoTweenOnCubeOnScene : MonoBehaviour {
    public float duration = 1f;
    private Material material;
    private Sequence sequence;

    void Start() {
        material = GetComponent<Renderer>().material;

        sequence = DOTween.Sequence();

        sequence.Append(DOTween.To(() => material.color, color => material.color = color, new Color(material.color.r, material.color.g, material.color.b, 0), duration).SetLoops(-1, LoopType.Yoyo));
    }
/*


    void Start() {
        Material material = GetComponent<MeshRenderer>().material;

        material.DOColor(new Color(material.color.r, material.color.g, material.color.b, 0), "_Color", duration)
            .SetLoops(-1, LoopType.Yoyo);
    }*/

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1);
            sequence.Kill();
        }
            
    }
}