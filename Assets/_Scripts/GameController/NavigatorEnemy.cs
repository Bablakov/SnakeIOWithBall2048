using System.Linq;

public class NavigatorEnemy {

    private Player _player;
    private ControllerUnits _controllerEnemy;
    private ControllerSections _controllerSection;

    public NavigatorEnemy(Player player, ControllerUnits controllerEnemy, ControllerSections controllerSection) {
        _player = player;
        _controllerEnemy = controllerEnemy;
        _controllerSection = controllerSection;
    }

    public Section FindTarget(Section sectionStart) {
        var general = _controllerSection.Objects.Concat(_controllerEnemy.Objects.Select(unit => unit.Head)).ToList();

        if (_player.gameObject.activeSelf) {
            general.Add(_player.Head);
        }

        if (general.Contains(sectionStart)) {
            general.Remove(sectionStart);
        }

        var beetwenCollection = general
            .OrderBy(section => (section.transform.position - sectionStart.transform.position).magnitude)
            .Where(section => {
                if (section.IsUnit && section.Level < sectionStart.Level)
                    return true;
                if (!section.IsUnit && section.Level <= sectionStart.Level)
                    return true;
                return false;
            }).ToList();

        var beetwen = beetwenCollection
            .First();

        return beetwen;
    }
}