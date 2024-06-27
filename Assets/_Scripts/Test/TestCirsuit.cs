using DG.Tweening;
using System;
using UnityEngine;

public class TestCirsuit : MonoBehaviour {
    [SerializeField] private TestDoTweenOnCubeOnScene[] testDoTweenOnCubeOnScenes;

    private void Start() {
        for (int i = 0; i < testDoTweenOnCubeOnScenes.Length; i++) {
            if (i != testDoTweenOnCubeOnScenes.Length - 1)
                testDoTweenOnCubeOnScenes[i].SetSequence(testDoTweenOnCubeOnScenes[i + 1].PlayAnimation);
            else
                testDoTweenOnCubeOnScenes[i].SetSequence(EndAnimation);
        }
        //testDoTweenOnCubeOnScenes[0].SetSequence(EndAnimation);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            /*for (int i = 0; i < testDoTweenOnCubeOnScenes.Length; i++) {
                if (i != testDoTweenOnCubeOnScenes.Length - 1)
                    testDoTweenOnCubeOnScenes[i].SetSequence(testDoTweenOnCubeOnScenes[i + 1].PlayAnimation);
                else
                    testDoTweenOnCubeOnScenes[i].SetSequence(EndAnimation);
            }*/
            testDoTweenOnCubeOnScenes[0].PlayAnimation();
        }
    }

    private void EndAnimation() {
        Debug.Log("EndAnimation");
    }
}