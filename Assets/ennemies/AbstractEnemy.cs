using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AbstractEnemy : MonoBehaviour {

    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected Vector2 direction;
    [SerializeField]
    protected int contactDamage;
    [SerializeField]
    protected int bonusScore;
    [SerializeField]
    protected bool dieOnCollision;
    public int life;

    [SerializeField]
    protected float timeout;
    protected float deathtime = 0;
    
    protected Rigidbody2D _rb2d;
    protected SpriteRenderer _renderer;
    protected float _blinkUntil;
    protected bool _blinking = false;

    protected bool alive = true; 

    public void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!GameManager.Instance.Running)
            return;

        if(alive && collision.collider.tag == "Player") {
            var health = collision.collider.gameObject.GetComponent<PlayerHealth>();
            health.RecieveDamage(contactDamage);
            if(dieOnCollision)
                OnDying();
        }
    }

    public void Update() {
        var current = Time.realtimeSinceStartup;
        if(_blinking && current > _blinkUntil) {
            _blinking = false;
            _renderer.color = Color.white;
        }

        if(_blinking) {
            if (_renderer.color == Color.white)
                _renderer.color = Color.red;
            else
                _renderer.color = Color.white;
        }

        if (transform.position.x < -11) Destroy(gameObject);
        if (!alive && Time.time > deathtime)
        {
            Debug.Log("AOZKDPOIAHZDFOIJAZDOji");
            GameObject.Destroy(gameObject);
        }
    }

    public void OnDying() {
        var gm = GameManager.Instance;
        gm.Score += (int)(gm.Combo * bonusScore);
        gm.IncreaseKillCount();
        var anim = GetComponent<Animator>();
        anim.SetBool("dead", true);
        alive = false;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        deathtime = Time.time + timeout;
    }

    public void Blink() {
        _blinking = true;
        _blinkUntil = Time.realtimeSinceStartup + 2f;
    }

}
