using System.Linq;
using UnityEngine;
using Zenject;

public class LeaderBoard : MonoBehaviour {
    private ViewLineLeaderBoard[] _linesLeaderBoard;
    private Player _player;
    private SectionConfig _sectionConfig;
    private ControllerUnits _controllerUnits;

    private float _currentTime = 0f;
    private float _timeUpdate = 5f;

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
        if (_currentTime > _timeUpdate) {
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

            _currentTime = 0f;
        }
        else {
            _currentTime += Time.deltaTime;
        }
    }
}