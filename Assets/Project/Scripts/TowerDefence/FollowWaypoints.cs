using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private string _pathName;
    [SerializeField] List<Vector3> _waypointsPosition = new List<Vector3>();
    [SerializeField] private float _distanceThreshhold = .5f;
    [SerializeField] private float speed = 1;
    private Coroutine _getNetWaypointRoutine;
    private int _currentWaypoint;
    private void OnEnable(){
        _currentWaypoint = 0;
        StartCoroutine(GetNextWaypoint());
    }
        
    private void OnDisable() => StopAllCoroutines();
    private void Awake() {
        _getNetWaypointRoutine = StartCoroutine(GetNextWaypoint());
    }
    void GetWaypoints()
    {
        Transform path = GameObject.Find(_pathName).transform;
        for (int i = 0;i < path.childCount; i++)
        {
            _waypointsPosition.Add(path.GetChild(i).position);
        }
    }
    IEnumerator GetNextWaypoint()
    {
        if (_waypointsPosition.Count == 0)
        {
            GetWaypoints();
        }
        float distance = Vector3.Distance(transform.position, _waypointsPosition[_currentWaypoint]);
        while (distance >  _distanceThreshhold && _gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypointsPosition[_currentWaypoint], Time.deltaTime * speed);
            distance = Vector3.Distance(transform.position, _waypointsPosition[_currentWaypoint]);
            yield return null;
        }
        if (_currentWaypoint < _waypointsPosition.Count - 1)
        {
            _currentWaypoint++;
            StartCoroutine(GetNextWaypoint());
        }
    }
}
