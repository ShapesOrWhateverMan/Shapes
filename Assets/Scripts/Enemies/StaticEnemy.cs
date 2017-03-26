using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy {

    void Start() {
        //Set stats
        stats.maxHealth = 1000f;       //TODO: Create another class of unkillable Enemy?
        stats.health = stats.maxHealth;
        stats.contactDamage = 20f;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        //If the projectile collides with a player
        if (coll.gameObject.tag == "Player") {
            coll.gameObject.SendMessage("DamagePlayer", stats.contactDamage, SendMessageOptions.DontRequireReceiver);
        }
        //TODO: If the projectile collides with any platform, destroy the projectile
        //This will require a dynamic rigid body on every platform
        /*
    else if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Platforms")) {
        Destroy(gameObject);
    }
    */
    }
}
