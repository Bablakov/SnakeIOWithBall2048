using System.Linq;
using UnityEngine;
using Zenject;

public class LeaderBoard : MonoBehaviour {
    private ViewLineLeaderBoard[] _linesLeaderBoard;
    private Player _player;
    private SectionConfig _sectionConfig;
    private ControllerUnits _controllerUnits;

    public void Initailize() {
        _linesLeaderBoard = GetComponentsInChildren<ViewLineLeaderBoard>();
    }

    [Inject]
    private void Construct(Player player, ControllerUnits controllerUnits, SectionConfig sectionConfig) {
        _player = player;
        _sectionConfig = sectionConfig;
        _controllerUnits = controllerUnits;
    }

    private void Update() {
        var list = _controllerUnits.Objects.ToList();
        list.Add(_player);

        var beetwen = list
            .OrderByDescending(unit => unit.Level)
            .ThenByDescending(unit => unit.Nickname)
            .Take(_linesLeaderBoard.Length)
            .ToArray();

        for (int i = 0; i < _linesLeaderBoard.Length; i++) {
            _linesLeaderBoard[i].SetValue((i+1).ToString(), beetwen[i].Nickname, _sectionConfig.Sections[beetwen[i].Level].Text);
        }
    }
}