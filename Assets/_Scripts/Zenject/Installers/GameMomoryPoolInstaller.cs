using UnityEngine;
using Zenject;

public class GameMomoryPoolInstaller : MonoInstaller {
    [SerializeField] private ViewLineKilledField killedField;
    [SerializeField] private Section section;
    [SerializeField] private Unit unit;

    public override void InstallBindings() {
        BindMemoryPool();
    }

    private void BindMemoryPool() {
        Container.BindMemoryPool<Section, SectionPool>().FromComponentInNewPrefab(section);
        Container.BindMemoryPool<Unit, UnitPool>().FromComponentInNewPrefab(unit);
        Container.BindMemoryPool<ViewLineKilledField, LineKillFieldPool>().FromComponentInNewPrefab(killedField);
    }
}