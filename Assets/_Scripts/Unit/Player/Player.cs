using UnityEngine;
using System;
using YG;

public class Player : Unit {
    public event Action KilledEnemy;
    public event Action DiedPlayer;
    public event Action PutOnSection;

    private ControlledElement _controlledElement;
    private ViewFreeSpeedUpOnPlayer _viewFreeSpeedUpOnPlayer;

    public override void Initialize(string nickname) {
        base.Initialize(nickname);
        Head.SetLevel(YandexGame.savesData.startLevelPlayer);
        Subscribe();
        TurnOnInvulnerable();   
    }

    public void Enable() {
        gameObject.SetActive(true);
        TurnOnInvulnerable();
    }

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElement = GetComponent<ControlledElement>();
        _viewFreeSpeedUpOnPlayer = GetComponentInChildren<ViewFreeSpeedUpOnPlayer>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElement.Initialize(ParametrsSnake, ControllerSpeedUp);
        _viewFreeSpeedUpOnPlayer.Initialize(ControllerSpeedUp);
        _viewFreeSpeedUpOnPlayer.Disable();
    }

    protected override void OnDiedMe() {
        base.OnDiedMe();
        gameObject.SetActive(false);
        DiedPlayer?.Invoke();
    }

    protected void OnDiedEnemy() {
        KilledEnemy?.Invoke();
    }

    private void OnAddedSection() {
        PutOnSection?.Invoke();
    }

    private void Subscribe() {
        CollisionHandler.DiedEnemy += OnDiedEnemy;
        CollisionHandler.DiedMe += OnDiedMe;
        ControllerStorageSection.AddedSection += OnAddedSection;
        ControllerSpeedUp.SpeededUp += OnSpeededUp;
        ControllerSpeedUp.SlowedDown += OnSlowedDown;        
    }

    private void Unsubscribe() {
        CollisionHandler.DiedEnemy -= OnDiedEnemy;
        CollisionHandler.DiedMe -= OnDiedMe;
        ControllerStorageSection.AddedSection -= OnAddedSection;
        ControllerSpeedUp.SpeededUp -= OnSpeededUp;
        ControllerSpeedUp.SlowedDown  -= OnSlowedDown;
    }

    private void OnSpeededUp() {
        _viewFreeSpeedUpOnPlayer.Enable();
    }

    private void OnSlowedDown() {
        _viewFreeSpeedUpOnPlayer.Disable();
    }

    protected override void OnDestroy() {
        Unsubscribe();
    }
}