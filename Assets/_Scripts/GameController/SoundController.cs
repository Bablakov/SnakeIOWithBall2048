using System;
using UnityEngine;

public class SoundController : MonoBehaviour, IDisposable{
    private SoundPickUpSection _soundPickUpSection;
    private SoundBackground _soundBackground;
    private SoundDeathPlayer _deathPlayer;
    private SoundDeathEnemy _deathEnemy;
    //private SoundButton _soundButton;

    public void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    private void GetComponents() {
        _soundPickUpSection = GetComponentInChildren<SoundPickUpSection>();
        _soundBackground = GetComponentInChildren<SoundBackground>();
        _deathPlayer = GetComponentInChildren<SoundDeathPlayer>();
        _deathEnemy = GetComponentInChildren<SoundDeathEnemy>();
        //_soundButton = GetComponentInChildren<SoundButton>();
    }

    private void InitializeComponents() {
        _soundPickUpSection.Initialize();
        _soundBackground.Initialize();
        _deathPlayer.Initialize();
        _deathEnemy.Initialize();
        //_soundButton.Initialize();
    }

    public void Dispose() {
        //_soundButton.Dispose();
    }
}