using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void PlayerCheckpoint()
	{
	}

	public IEnumerator PlayerHitCheckpointCo(int bonus)
	{
		yield break;
	}

	public void PlayerLeftCheckpoint()
	{
	}

	public void SpawnPlayer(shapes player)
	{
		player.RespawnAt (transform);
	}

	public void AssignObjectToCheckpoint()
	{
	}

	// Update is called once per frame
	void Update () {
		
	}
}
