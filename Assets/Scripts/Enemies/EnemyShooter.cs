using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy {

    public Transform target;//TODO: let ShooterAI assign the target
    public float fireRate = 0.5f;
    public float bulletDamage = 30f;
    public float triggerRadius = 20f;
    public float nextTimeToSearch = 0;
    //public int fireRotationOffset = 90;
    public LayerMask hitLayer;

    float timeToFire = 0;
    Transform firePoint;
    public Transform BulletTrailPrefab;

    void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("Firepoint is null");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) {
            FindPlayer();
            return;
        }

        //Death by falling
        if (transform.position.y <= -30) {
            DamageEnemy(999999);
        }

        //Aim and shoot at target if within radius
        Vector3 difference = target.position - transform.position;
        if (difference.magnitude > triggerRadius) {
            target = null;
            return; //Or patrol
        }
        //Always aim at target
        AimAtTarget();
        //TODO: Check if target is within detection radius
        //Laser, basically
        if (fireRate == 0) {
            Shoot();
        }
        //Single bullets
        else {
            if (Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot() {
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, targetPos - firePointPos, 100, hitLayer);
        BulletEffect();
        if (hit.collider != null) {
            //Player is hit! Deal damage equal to bulletDamage
        }
    }

    void BulletEffect() {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }

    void AimAtTarget() {
        Vector3 difference = target.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void FindPlayer() {
        if (nextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null) {
                target = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
