using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Waypoint : MonoBehaviour {
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    void Start() {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWaypointPosition(int index) {
        return CurrentPosition + Points[index];
    }
    private void OnDrawGizmos() {
        if (!_gameStarted && transform.hasChanged) {
            _currentPosition = transform.position;
        }

        for (int index = 0; index < points.Length; index++) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(points[index] + _currentPosition, 0.5f);
            if (index < points.Length - 1) {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[index] + _currentPosition, points[index + 1] + _currentPosition);
            }
        }
    }
}