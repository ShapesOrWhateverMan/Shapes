using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
    public class EnemyStats {
        public float health = 100f;
        public float contactDamage = 20f;
    }

    public EnemyStats stats = new EnemyStats();

    public void DamageEnemy(float damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            GameMaster.KillEnemy(this);
        }
    }
}
