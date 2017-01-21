using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    protected float _projSpeed;
    protected float _lifeTime;
    protected Vector2 _direction;
    protected Rigidbody2D _rb2D;

    public void SetProjSpeed(float speed) {
        _projSpeed = speed;
    }

    public void SetLifeTime(float lifeTime) {
        _lifeTime = lifeTime;
    }

	void Start () {
        _direction = gameObject.transform.up;
        _rb2D = GetComponent<Rigidbody2D>();
	}
	
    void Update() {
        if (Time.realtimeSinceStartup > _lifeTime)
            GameObject.Destroy(gameObject);
    }

	void FixedUpdate () {
        _rb2D.velocity = _direction * _projSpeed * Time.fixedDeltaTime;
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            Debug.Log("Impact");
            GameObject.Destroy(gameObject);
        }
    }
}
