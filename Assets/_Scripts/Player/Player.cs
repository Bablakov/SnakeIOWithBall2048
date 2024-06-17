using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour {
    [SerializeField] ParametrsPlayer parametrsPlayer;
    [SerializeField] public List<Section> bodyElements;

    private ControlledElement _controlledElement;
    private FollowingElements _followingElements;

    [Inject]
    private void Construct() {
        InitializeComponents();
    }

    private void Start() {
        CreateComponents();
        GetComponents();
    }

    private void CreateComponents() {
        /*_inputGame = new InputDesktop();*/
    }

    private void GetComponents() {
        _controlledElement = GetComponentInChildren<ControlledElement>();
        _followingElements = GetComponentInChildren<FollowingElements>();
    }

    private void InitializeComponents() {
        _controlledElement.Initialize(parametrsPlayer, this);
        _followingElements.Initialize(parametrsPlayer, this);
    }
}