using UnityEngine;
using DG.Tweening;

public class TestAnimationMerge : MonoBehaviour {
    [SerializeField, Range(0.1f, 2f)] private float durationAnimation;
    [SerializeField] private Transform sectionUpgraded;
    [SerializeField] private Transform sectionDeleted;

    private Sequence sequence;

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            MergeSection();
            Debug.Log("PressSpace");
        }*/
    }

    private void MergeSection() {
        sequence = DOTween.Sequence();

        sequence.Append(sectionDeleted.transform.DOMove(sectionUpgraded.transform.position, durationAnimation));
        sequence.OnComplete(OnAnimationComplete);
    }

    private void OnAnimationComplete() {
        Debug.Log($"MergedSections");
    }
}