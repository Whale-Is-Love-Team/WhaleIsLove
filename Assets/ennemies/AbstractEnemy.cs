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
    protected GameObject fadingText;

    protected Rigidbody2D _rb2d;

    public void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!GameManager.Instance.Running)
            return;

        if(collision.collider.tag == "Player") {
            var health = collision.collider.gameObject.GetComponent<PlayerHealth>();
            health.RecieveDamage(contactDamage);
            GameObject.Destroy(gameObject);
        }
    }

    public void OnDying() {
        var gm = GameManager.Instance;
        gm.Score += (int) (gm.Combo * bonusScore);
        gm.IncreaseKillCount();
        GameObject.Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log("collision abstraite");
    }

}
