using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProjBehaviour : MonoBehaviour {

    protected Sprite _sprite;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rb2D;
    protected float _moveSpeed = 100f;
    protected Vector2 _direction = Vector2.right;

    public void SetMoveSpeed(float newSpeed) {
        _moveSpeed = newSpeed;
    }

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _sprite = _renderer.sprite;
        _rb2D = GetComponent<Rigidbody2D>();
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Enemy") {
            GameObject.Destroy(collider.gameObject);
        }
    }

    void FixedUpdate() {
        _rb2D.velocity = _direction * _moveSpeed * Time.fixedDeltaTime;
    }
}
