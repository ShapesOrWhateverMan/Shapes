using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEnvStuff : MonoBehaviour {

	private shapes player;

	void Start(){
		player = gameObject.GetComponentInParent<shapes> ();
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Spring") {
			player.setRigBody2D (new Vector2(player.getRigidBody2D().velocity.x, 50f));
		}
	}

	void OnCollisionExit2D(Collision2D coll){
	
	}
}
