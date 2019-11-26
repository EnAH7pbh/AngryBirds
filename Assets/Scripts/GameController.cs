using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public SlingShooter slingshooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public bool _isGameEnded = false;
    // Start is called before the first frame update
    void Start () {
        for (int i = 0; i < Birds.Count; i++) {
            Birds[i].OnBirdDestroyed += ChangeBird;
        }
        for (int i = 0; i < Enemies.Count; i++) {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        slingshooter.InitiateBird (Birds[0]);
    }

    public void ChangeBird () {
        if (_isGameEnded) {
            return;
        }
        Birds.RemoveAt (0);
        if (Birds.Count > 0) {
            slingshooter.InitiateBird (Birds[0]);
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
}