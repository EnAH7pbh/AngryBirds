using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour {
    public CircleCollider2D Collider;
    public LineRenderer Trajectory;
    private Vector2 _startPos;
    [SerializeField] private float _radius = 0.75f;
    [SerializeField] private float _throwSpeed = 30f;
    private Bird _bird;
    // Start is called before the first frame update
    void Start () {
        _startPos = transform.position;
    }

    void OnMouseUp () {
        Collider.enabled = false;
        Vector2 velocity = _startPos - (Vector2) transform.position;
        float distance = Vector2.Distance (_startPos, transform.position);

        _bird.Shoot (velocity, distance, _throwSpeed);

        //restore catapult
        gameObject.transform.position = _startPos;

        Trajectory.enabled = false;
    }

    void OnMouseDrag () {
        //change mouse position to world position
        Vector2 p = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        //calculate so the catapult in radius range
        Vector2 dir = p - _startPos;
        if (dir.sqrMagnitude > _radius) {
            dir = dir.normalized * _radius;
        }
        transform.position = _startPos + dir;

        float distance = Vector2.Distance (_startPos, transform.position);

        if (!Trajectory.enabled) {
            Trajectory.enabled = true;
        }
        DisplayTrajectory (distance);
    }

    public void InitiateBird (Bird bird) {
        _bird = bird;
        _bird.MoveTo (gameObject.transform.position, gameObject);
        Collider.enabled = true;
    }

    void DisplayTrajectory (float distance) {
        if (_bird == null) {
            return;
        }

        Vector2 velocity = _startPos - (Vector2) transform.position;
        int segmentCount = 5;
        Vector2[] segments = new Vector2[segmentCount];

        //start position trajectory equal to mousu position
        segments[0] = transform.position;

        //start velocity
        Vector2 segVelocity = velocity * _throwSpeed * distance;

        for (int i = 1; i < segmentCount; i++) {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = Physics2D.gravity * elapsedTime * elapsedTime * 0.5f + segVelocity * elapsedTime + segments[0];
        }

        Trajectory.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++) {
            Trajectory.SetPosition (i, segments[i]);
        }
    }
}