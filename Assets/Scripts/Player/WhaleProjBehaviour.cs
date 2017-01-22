using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProjBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject coeur;
    [SerializeField]
    private Component blur;
    
    protected Sprite _sprite;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rb2D;
    protected float _moveSpeed = 100f;
    protected Vector2 _direction = Vector2.right;
    protected float _lifeTime;
    protected float _programmedDeath;

    public void SetMoveSpeed(float newSpeed) {
        _moveSpeed = newSpeed;
    }

    public void SetLifeTime(float lifeTime) {
        _lifeTime = lifeTime;
        _programmedDeath = Time.realtimeSinceStartup + lifeTime;
    }

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _sprite = _renderer.sprite;
        _rb2D = GetComponent<Rigidbody2D>();
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Enemy") {
            var enemy = collider.gameObject.GetComponent<AbstractEnemy>();
            if (enemy == null)
                return;
            var position = enemy.transform.position;
            enemy.OnDying();
            var coeurInstance = Instantiate(coeur);
            coeurInstance.transform.position = position;
        }
    }

    void Update() {
        var current = Time.realtimeSinceStartup;
        if(current > _programmedDeath) {
            GameObject.Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        _rb2D.velocity = _direction * _moveSpeed * Time.fixedDeltaTime;
    }
}
