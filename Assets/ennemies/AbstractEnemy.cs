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

    protected Rigidbody2D _rb2d;

    public void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (!GameManager.Instance.Running)
            return;

        if(collider.tag == "Player") {
            var health = collider.gameObject.GetComponent<PlayerHealth>();
            health.RecieveDamage(contactDamage);
            GameObject.Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log("collision abstraite");
    }

}
