using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField]
    private Vector3[] _points;

    public Vector3[] Points => _points;
    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    void Start() {
        _gameStarted = true;
        _currentPosition = transform.position;
    }
    
    void Update() { }

    private void OnDrawGizmos() {
        if (!_gameStarted && transform.hasChanged) {
            _currentPosition = transform.position;
        }

        for (int index = 0; index < _points.Length; index++) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_points[index] + _currentPosition, 0.5f);
            if (index < _points.Length - 1) {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(_points[index] + _currentPosition, _points[index + 1] + _currentPosition);
            }
        }
    }
}