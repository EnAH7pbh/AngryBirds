using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour {
    public enum BirdState { Idle, Thrown, HitSomething }
    public GameObject parent;
    public Rigidbody2D Rigidbody;
    public Collider2D Collider;
    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;
    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };
    public BirdState State { get { return _state; } }
    // Start is called before the first frame update
    void Start () {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        _state = BirdState.Idle;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (_state == BirdState.Idle && Rigidbody.velocity.sqrMagnitude >= _minVelocity) {
            _state = BirdState.Thrown;
        }
        if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) && Rigidbody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy) {
            //destroy gameobject after 2 second
            //if speed < _minVelocity
            _flagDestroy = true;
            StartCoroutine (DestroyAfter (2));
        }
    }

    private IEnumerator DestroyAfter (float second) {
        yield return new WaitForSeconds (second);
        Destroy (gameObject);
    }

    public void MoveTo (Vector2 target, GameObject parent) {
        gameObject.transform.SetParent (parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot (Vector2 velocity, float distance, float speed) {
        Collider.enabled = true;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.velocity = velocity * speed * distance;
        OnBirdShot (this);
    }

    void OnDestroy () {
        OnBirdDestroyed ();
    }

    void OnCollisionEnter2D (Collision2D collision) {
        _state = BirdState.HitSomething;
    }

    public virtual void OnTap(){
        //nothing
    }
}