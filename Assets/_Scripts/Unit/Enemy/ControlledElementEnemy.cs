using UnityEngine;
using UnityEngine.AI;

public class ControlledElementEnemy : MonoBehaviour {
    private const int SPEED_MULTIPLIER = 2;

    private ParametrsSnake _parametrsSnake;
    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    private float _currentTime;
    private float _standartTime;

    public void Initialize(ParametrsSnake parametrsSnake, Transform player, NavMeshAgent navMeshAgent) {
        _parametrsSnake = parametrsSnake;
        _navMeshAgent = navMeshAgent;
        _player = player;
    }

    private void Update() {
        if (_currentTime > _standartTime) {
            _navMeshAgent.SetDestination(_player.position);
            _currentTime = 0f;
        }
        else {
            _currentTime += Time.deltaTime;
        }
    }

    private void Subscribe() {
    }

    private void Unsubscribe() {
    }

    private void OnSpededUp() {
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed * SPEED_MULTIPLIER);
    }

    private void OnSpeededDown() {
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed / SPEED_MULTIPLIER);
    }
}