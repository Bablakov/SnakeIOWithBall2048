using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SectionConfig", menuName = "Configs/SectionConfig")]
public class SectionConfig : ScriptableObject {
    [SerializeField] private List<SectionInfo> sections;

    public IReadOnlyList<SectionInfo> Sections => sections;
}