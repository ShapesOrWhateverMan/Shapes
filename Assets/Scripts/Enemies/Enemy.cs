using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
    public class EnemyStats {
        public float maxHealth;
        public float health;
        public float contactDamage;
    }

    public EnemyStats stats = new EnemyStats();

    void Awake() {
        //Set stats
        stats.maxHealth = 100f;
        stats.health = stats.maxHealth;
    }

    virtual public void DamageEnemy(float damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            GameMaster.KillEnemy(this);
        }
    }
}
