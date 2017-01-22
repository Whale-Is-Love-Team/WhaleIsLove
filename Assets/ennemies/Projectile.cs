using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    protected float _projSpeed;
    protected float _lifeTime;
    protected Vector2 _direction;
    protected Rigidbody2D _rb2D;
    protected int _damage;

    public void SetProjSpeed(float speed) {
        _projSpeed = speed;
    }

    public void SetLifeTime(float lifeTime) {
        _lifeTime = lifeTime;
    }

    public void SetDamage(int damage) {
        _damage = damage;
    }
	void Start () {
        _direction = gameObject.transform.up;
        _rb2D = GetComponent<Rigidbody2D>();
	}
	
    void Update() {
        if (!GameManager.Instance.Running || Time.realtimeSinceStartup > _lifeTime)
            GameObject.Destroy(gameObject);
    }

	void FixedUpdate () {
        _rb2D.velocity = _direction * _projSpeed * Time.fixedDeltaTime;
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            var health = collider.gameObject.GetComponent<PlayerHealth>();
            health.RecieveDamage(_damage);
            GameObject.Destroy(gameObject);
        }
    }
}
