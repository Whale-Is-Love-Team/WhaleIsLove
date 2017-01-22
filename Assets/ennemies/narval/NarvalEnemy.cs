using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NarvalEnemy : AbstractEnemy {

    // Exposed fields
    [SerializeField]
    protected int hornDamage = 1;
    [SerializeField]
    [Tooltip("time between attacks in seconds")]
    protected float hornDelay = 2f;
    [SerializeField]
    [Tooltip("speed of the projectiles in unit / seconds")]
    protected float projectileSpeed = 20f;
    [SerializeField]
    protected float projectileDistance = 40f;
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected GameObject hornLauncher;

    protected float _nextAttackAt = 0;
    protected GameObject _player;

    new void Start() {
        base.Start();
        direction = Vector2.left;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    new void Update() {
        base.Update();

        var currentTime = Time.realtimeSinceStartup;

        if (currentTime > _nextAttackAt) {
            _nextAttackAt = currentTime + hornDelay;

            var projLifeTime = currentTime + projectileDistance / projectileSpeed;
            var obj = GameObject.Instantiate(projectile, hornLauncher.transform.position, projectile.transform.localRotation, null);
            var proj = obj.GetComponent<Projectile>();
            proj.SetProjSpeed(projectileSpeed);
            proj.SetLifeTime(projLifeTime);
            proj.SetDamage(hornDamage);
        }
    }

    void FixedUpdate() {
        _rb2d.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }
}
