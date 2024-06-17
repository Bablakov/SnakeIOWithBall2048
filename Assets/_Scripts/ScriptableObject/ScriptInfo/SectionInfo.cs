using System;
using UnityEngine;

[Serializable]
public class SectionInfo {
    [SerializeField] private Color color;
    [SerializeField] private string text;

    public Color ColorSection => color;
    public string Text => text;
}