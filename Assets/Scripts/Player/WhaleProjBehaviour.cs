using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProjBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject coeur;

    protected Sprite _sprite;
    protected SpriteRenderer _renderer;
    protected Rigidbody2D _rb2D;
    protected float _moveSpeed = 100f;
    protected Vector2 _direction = Vector2.right;
    protected float _lifeTime;
    protected float _programmedDeath;


    private float colorEndtime = 0;
    [SerializeField]
    private float colorTimeout = 0;
    private MonoBehaviour color;
    private float blurEndtime = 0;
    [SerializeField]
    private float blurTimeout = 0;
    private MonoBehaviour blur;

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
        color = (MonoBehaviour)Camera.main.GetComponent("ColorCorrectionCurves");
        if (color != null) color.enabled = false;
        blur = (MonoBehaviour)Camera.main.GetComponent("MotionBlur");
        if (blur != null) blur.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Enemy") {
            var enemy = collider.gameObject.GetComponent<AbstractEnemy>();
            if (enemy == null)
                return;

            var position = enemy.transform.position;
            var coeurInstance = Instantiate(coeur);
            coeurInstance.transform.position = position;

            if (color != null)
            {
                color.enabled = true;
                colorEndtime = Time.time + colorTimeout;
            }

            if (blur != null)
            {
                blur.enabled = true;
                blurEndtime = Time.time + blurTimeout;
            }
            enemy.life--;

            if (enemy.life <= 0)
                enemy.OnDying();
            else
                enemy.Blink();
        }
    }

    void Update() {
        var current = Time.realtimeSinceStartup;
        if(current > _programmedDeath) {
            GameObject.Destroy(gameObject);
        }
        if (color != null && Time.time > colorEndtime) color.enabled = false;
        if (blur != null && Time.time > blurEndtime) blur.enabled = false;
    }

    void FixedUpdate() {
        _rb2D.velocity = _direction * _moveSpeed * Time.fixedDeltaTime;
    }
}
