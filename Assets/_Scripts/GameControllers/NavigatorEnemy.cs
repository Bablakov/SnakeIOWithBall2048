using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class NavigatorEnemy {
    private const float RADIUS_SEARCH_TARGET = 15f;

    private Player _player;
    private ControllerUnits _controllerUnits;
    private ControllerSections _controllerSection;

    [Inject]
    public NavigatorEnemy(Player player, ControllerUnits controllerEnemy, ControllerSections controllerSection) {
        _player = player;
        _controllerUnits = controllerEnemy;
        _controllerSection = controllerSection;
    }

    public Transform FindTarget(Unit searcher) {
        var units = _controllerUnits.Objects.ToList();
        var sections = _controllerSection.Objects;
        return FindTarget(units, sections, searcher);
    }

    private Transform FindTarget(List<Unit> units, IEnumerable<Section> sections, Unit searcher) {
        var section = SearchBestTargetSection(sections, searcher);
        var unit = SearchBestTargetUnit(units, searcher);

        return DetermineBest(section, unit);
    }

    private Section SearchBestTargetSection(IEnumerable<Section> sections, Unit searcher) {
        var possibleVariants = sections
            .Where(section => section.Level <= searcher.Level);

        return SearchElementInRadius(searcher.transform, possibleVariants);
    }

    private Unit SearchBestTargetUnit(List<Unit> units, Unit searcher) {
        if (_player.gameObject.activeSelf) {
            units.Add(_player);
        }

        if (units.Contains(searcher)) {
            units.Remove(searcher);
        }

        var possibleVariants = units
            .Where(unit => unit.Level < searcher.Level);

        return SearchElementInRadius(searcher.transform, possibleVariants);
    }

    private static Transform DetermineBest(Section section, Unit unit) {
        if (unit != null && section != null) {
            if (section.Level >= unit.Level) {
                return section.transform;
            } else {
                return unit.transform;
            }
        } 
        else if (section != null) {
            return section.transform;
        } 
        else if (unit != null) {
            return unit.transform;
        } 
        else { 
            return null; 
        }
    }

    private static T SearchElementInRadius<T>(Transform searcher, IEnumerable<T> generalUnit) where T : MonoBehaviour {
        return generalUnit
            .Where(unit => (unit.transform.position - searcher.transform.position).magnitude < RADIUS_SEARCH_TARGET)
            .OrderBy(unit => (unit.transform.position - searcher.transform.position).magnitude)
            .FirstOrDefault();
    }
}