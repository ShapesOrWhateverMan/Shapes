using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IceBall : EnemyProjectile {
    private SpriteRenderer spriteRendr;
    public Transform target = null;
    public float fadeInRate = 1.05f;
    public float fadeOutRate = 0.95f;
    public int moveSpeed = 6;
    private float currColor;
    private bool isFadingOut = true;
    private int lifeTime;
    public float nextTimeToSearch = 0;

    // Use this for initialization
    void Start() {
        spriteRendr = this.GetComponent<SpriteRenderer>();
        stats.contactDamage = 25f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
        currColor = 255;
        lifeTime = 7;
    }

    // Update is called once per frame
    void Update() {
        if (gameObject != null) {
            if (target == null) {
                Destroy(gameObject);
            }
            else {
                //To activate homing missile:
                //AimAtTarget();
                FindPlayer();
                Vector3 difference = target.position - transform.position;
                difference.Normalize();
                transform.Translate(difference * Time.deltaTime * moveSpeed);
                float r = spriteRendr.color.r;
                float g = spriteRendr.color.g;
                float b = spriteRendr.color.b;

                //Constantly change color
                if (currColor < 25f)
                    isFadingOut = false;
                else if (currColor > 225f)
                    isFadingOut = true;

                if (isFadingOut) {
                    currColor = r * fadeOutRate;
                    spriteRendr.color = new Color(currColor, g, b, 1f);
                }
                else {
                    currColor = r * fadeInRate;
                    spriteRendr.color = new Color(currColor, g, b, 1f);
                }
                Destroy(gameObject, lifeTime);        
            }
        }
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
            nextTimeToSearch = Time.time + 1f;
        }
    }
}
