using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public SlingShooter slingshooter;
    public TrailController trailcontroller;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public bool _isGameEnded = false;
    public BoxCollider2D TapCollider ;
    public Bird _shotBird;
    // Start is called before the first frame update
    void Start () {
        for (int i = 0; i < Birds.Count; i++) {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < Enemies.Count; i++) {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        slingshooter.InitiateBird (Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird () {
        TapCollider.enabled = false;
        if (_isGameEnded) {
            return;
        }
        Birds.RemoveAt (0);
        if (Birds.Count > 0) {
            slingshooter.InitiateBird (Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void CheckGameEnd (GameObject DestroyedEnemy) {
        for (int i = 0; i < Enemies.Count; i++) {
            if (Enemies[i].gameObject == DestroyedEnemy) {
                Enemies.RemoveAt (i);
                break;
            }
        }
        if (Enemies.Count == 0) {
            _isGameEnded = true;
        }
    }

    public void AssignTrail (Bird bird) {
        trailcontroller.SetBird (bird);
        StartCoroutine (trailcontroller.SpawnTrail ());
        TapCollider.enabled = true;
    }

    void OnMouseUp () {
        if (_shotBird != null) {
            _shotBird.OnTap ();
        }
    }
}