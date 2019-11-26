using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
    public float Health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };
    private bool _isHit = false;
    // Start is called before the first frame update
    void OnDestroy () {
        if (_isHit) {
            OnEnemyDestroyed (gameObject);
        }
    }

    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.GetComponent<Rigidbody2D> () == null) {
            return;
        }
        if (col.gameObject.tag == "Bird") {
            _isHit = true;
            Destroy (gameObject);
        }
        if (col.gameObject.tag == "Obstacle") {
            //calculate damage taken
            float damage = col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude * 10;
            Health -= damage;
            if (Health <= 0) {
                _isHit = true;
                Destroy (gameObject);
            }
        }
    }
}