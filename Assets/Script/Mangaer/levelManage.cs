using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class levelManage : MonoBehaviour {

	public static levelManage instance { get; private set;}

	public shapes player { get; private set;}
	public CameraFollow camera { get; private set;}

	private List<Checkpoint> _checkpoints;
	private int _currentCheckpointIndex;

	public Checkpoint debugSpawn;

	public void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		_checkpoints = FindObjectsOfType<Checkpoint> ().OrderBy(t => t.transform.position.x).ToList();
		_currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

		player = FindObjectOfType<shapes> ();
		camera = FindObjectOfType<CameraFollow> ();
	
	#if UNITY_EDITOR
		if (debugSpawn != null)
			debugSpawn.SpawnPlayer (player);
		else if (_currentCheckpointIndex != -1)
			_checkpoints [_currentCheckpointIndex].SpawnPlayer (player);
	#else
		if(_currentCheckpointIndex != -1)
			_checkpoints[_currentCheckpointIndex].SpawPlayer(player);
	#endif	
	}

	public void Update(){
		var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
		if (isAtLastCheckpoint)
			return;

		var distanceToNextCheckpoint = _checkpoints [_currentCheckpointIndex + 1].transform.position.x - player.transform.position.x;
		if (distanceToNextCheckpoint >= 0)
			return;

		_checkpoints [_currentCheckpointIndex].PlayerLeftCheckpoint ();
		_currentCheckpointIndex++;
		//_checkpoints [_currentCheckpointIndex].PlayerHitCheckpoint ();

		//1000: time bonus
	}

	public void killPlayer(){
		StartCoroutine (killPlayerCo ());
	}

	private IEnumerator killPlayerCo(){
		player.kill ();
		camera.IsFollowing = false;

		yield return new WaitForSeconds (2f);

		camera.IsFollowing = true;

		if (_currentCheckpointIndex != -1)
			_checkpoints [_currentCheckpointIndex].SpawnPlayer (player);
	
		//TODO: points
	}
}
