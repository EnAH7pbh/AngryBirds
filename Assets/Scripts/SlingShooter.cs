using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour {
    public CircleCollider2D Collider;
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
    }

    public void InitiateBird (Bird bird) {
        _bird = bird;
        _bird.MoveTo (gameObject.transform.position, gameObject);
        Collider.enabled = true;
    }
}