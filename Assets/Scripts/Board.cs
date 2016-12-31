using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    public float boardSpeed = 0.5f;
    public Vector3 playerPos;


	
	// Update is called once per frame
	void Update () {
        float x = gameObject.transform.position.x + Input.GetAxis("Horizontal") * boardSpeed;
        playerPos = new Vector3(Mathf.Clamp(x, -2.5f, 2.5f), -4, 0);
        gameObject.transform.position = playerPos;

	}


}
