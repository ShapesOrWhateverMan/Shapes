using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A singleton Game Master
public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    // Use this for initialization
    void Start() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int respawnDelay = 2;
    public Transform spawnPrefab; //respawn effect

    public IEnumerator RespawnPlayer() {
        //TODO: Respawn sound
        yield return new WaitForSeconds(respawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        //Respawn effect
        Transform particleClone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(particleClone.gameObject, 3f);
    }

	public static void KillPlayer(Shapes shape){
        Destroy(shape.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy) {
        Destroy(enemy.gameObject);
    }
}
