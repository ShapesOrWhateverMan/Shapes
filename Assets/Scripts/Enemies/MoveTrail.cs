using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 10;

	// Update is called once per frame
	void Update () {
        //TODO: set Vector3 targetDirection to replace Vector3.left below
        //if (gameObject != null)
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        //Destroy(gameObject, 3);
	}
}
