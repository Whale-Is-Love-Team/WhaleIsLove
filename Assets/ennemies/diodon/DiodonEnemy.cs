using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DiodonEnemy : AbstractEnemy {

    // Exposed fields
    [SerializeField]
    protected int spikesDamage = 1;
    [SerializeField]
    [Tooltip("time between attacks in seconds")]
    protected float spikesDelay = 2f;
    [SerializeField]
    [Tooltip("time of the attack move")]
    protected float spikesAttackTime = 1f;
    [SerializeField]
    [Tooltip("speed of the projectiles in unit / seconds")]
    protected float projectileSpeed = 20f;
    [SerializeField]
    [Tooltip("number of send projectile")]
    protected int projectileNumber = 8;
    [SerializeField]
    protected float projectileDistance = 40f;
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected float slowFactor = 0.1f;

    protected float _nextAttackAt = 0;
    protected float _staticUntil;
    protected bool _dontMove = false;
    protected GameObject _player;

    new void Start() {
        base.Start();
        direction = Vector2.left;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    new void Update () {
        base.Update();

        var currentTime = Time.realtimeSinceStartup;
        if(currentTime > _staticUntil) {
            _dontMove = false;
        }

        if (currentTime > _nextAttackAt && Vector2.Distance(gameObject.transform.position, _player.transform.position) < 5) {
            _staticUntil = currentTime + spikesAttackTime;
            _nextAttackAt = currentTime + spikesDelay;
            _dontMove = true;

            var arcDivision = 360 / projectileNumber;
            var projLifeTime = currentTime + projectileDistance / projectileSpeed;
            for (var i = 0; i < projectileNumber;  i++) {
                var obj = GameObject.Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0, 0, i * arcDivision), null);
                var proj = obj.GetComponent<Projectile>();
                proj.SetProjSpeed(projectileSpeed);
                proj.SetLifeTime(projLifeTime);
                proj.SetDamage(spikesDamage);
            }
        }
	}

    void FixedUpdate() {
        if(!_dontMove) {
            _rb2d.velocity = direction * moveSpeed * Time.fixedDeltaTime;
        }
        else {
            // @TODO : add slow before attack
            //var factor = (1f - slowFactor);
            //if (factor < 0)
            //    factor = 0;
            //_rb2d.velocity = _rb2d.velocity * factor;

            _rb2d.velocity = Vector2.zero;
        }
    }
}
    