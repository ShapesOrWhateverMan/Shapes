using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Lightning : Shapes {
    public float lightningDamage = 35f;
    public LayerMask hitLayer;

    float abilityCooldown = 3f;       //Acts as ability cooldown
    float timeToFire = 0;
    Transform firePoint;
    public Transform LightningTrailPrefab;
    public float lightningStrikeWidth = 1f;

    void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("Lightning Firepoint is null");
        }
        setHP(100);
    }

    void Start() {
        isGoodOrBad(true);
        base.Start();
        //setCharPos(3); //Doughnut is number three in the doc
    }

    void Update() {
        //Shapes class update
        base.Update();

        //Use ability (lightning strike)
        if (CrossPlatformInputManager.GetButtonDown("Ability")) {
            //Prevent ability spamming
            if (Time.time > timeToFire) {
                timeToFire = Time.time + abilityCooldown;
                if (facingRight)
                    firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                else {
                    firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
                Ability();
            }
        }
    }

    public void Ability() {
        //The attack becomes more lethal if the attack connects fully with the enemy (up to x3 dmg)
        Vector2 firePointPosMid = new Vector2(firePoint.position.x, firePoint.position.y);
        Vector2 firePointPosTop = new Vector2(firePointPosMid.x, firePointPosMid.y + lightningStrikeWidth / 2);
        Vector2 firePointPosBot = new Vector2(firePointPosMid.x, firePointPosMid.y - lightningStrikeWidth / 2);
        Vector2 hitDirection = facingRight? Vector2.right : Vector2.left;
        RaycastHit2D[] hits;
        
        BulletEffect();

        hits = Physics2D.RaycastAll(firePointPosTop, hitDirection, 100, hitLayer);
        //Hit by the top part of lightning
        foreach(RaycastHit2D hit in hits){
            hit.collider.SendMessage("DamageEnemy", lightningDamage, SendMessageOptions.DontRequireReceiver);
            Debug.DrawRay(firePointPosTop, hitDirection, Color.red);
            Debug.Log("Damage top: " + lightningDamage);
        }

        //Hit by the middle part of lightning
        hits = Physics2D.RaycastAll(firePointPosMid, hitDirection, 100, hitLayer);
        foreach (RaycastHit2D hit in hits) {
            hit.collider.SendMessage("DamageEnemy", lightningDamage, SendMessageOptions.DontRequireReceiver);
            Debug.DrawRay(firePointPosMid, hitDirection, Color.red);
            Debug.Log("Damage mid: " + lightningDamage);
        }

        //Hit by the bottom part of lightning
        hits = Physics2D.RaycastAll(firePointPosBot, hitDirection, 100, hitLayer);
        foreach (RaycastHit2D hit in hits) {
            hit.collider.SendMessage("DamageEnemy", lightningDamage, SendMessageOptions.DontRequireReceiver);
            Debug.DrawRay(firePointPosBot, hitDirection, Color.red);
            Debug.Log("Damage bot: " + lightningDamage);
        }
    }

    void BulletEffect() {
        Transform lightningClone = Instantiate(LightningTrailPrefab, firePoint.position, firePoint.rotation);
        //lightningClone.parent = firePoint;
        Destroy(lightningClone.gameObject,1);
    }
}