﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IceShard : EnemyProjectile {
    private SpriteRenderer spriteRendr;
    public Transform target = null;
    public float fadeInRate = 1.05f;
    public int moveSpeed = 30;
    private float currColor;

	// Use this for initialization
	void Start () {
        spriteRendr = this.GetComponent<SpriteRenderer>();
        stats.contactDamage = 25f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
            target = player.transform;
        currColor = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject != null) {
            if (target == null) {
                Destroy(gameObject);
            }
            else {
                //To activate homing missile:
                //AimAtTarget();
                if (currColor < 250) {
                    AimAtTarget();
                    float r = spriteRendr.color.r;
                    float g = spriteRendr.color.g;
                    float b = spriteRendr.color.b;
                    spriteRendr.color = new Color(r * fadeInRate, g * fadeInRate, b * fadeInRate, 1f);
                    currColor = r;
                }
                else {
                    transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                    Destroy(gameObject, 2f);
                }
            }
        }
	}

    void AimAtTarget() {
        Vector3 difference = target.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
