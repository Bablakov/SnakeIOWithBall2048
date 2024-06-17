using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField, Range(0.1f, 100f)] private float speed = 1f;
    [SerializeField, Range(0.1f, 10f)] private float distanceBeetwen = 1f;
    [SerializeField] public List<Section> bodyElements;

    private InputGame _inputGame;
    private ControlledElement _controlledElement;
    private FollowingElements _followingElements;

    private void Start() {
        CreateComponents();
        GetComponents();
        InitializeComponents();
    }

    private void CreateComponents() {
        _inputGame = new InputDesktop();
    }

    private void GetComponents() {
        _controlledElement = GetComponentInChildren<ControlledElement>();
        _followingElements = GetComponentInChildren<FollowingElements>();
    }

    private void InitializeComponents() {
        _controlledElement.Initialize(_inputGame, speed, this);
        _followingElements.Initialize(speed, distanceBeetwen, this);
    }
}