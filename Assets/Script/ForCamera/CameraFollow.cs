using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	public float smoothLineX;
	public float smoothLineY;

	private GameObject player;

	public BoxCollider2D Bounds;

	public Transform player1;

	public Vector2 margin, smoothing;
	private Vector2 _min, _max;

	public bool IsFollowing { get; set; }

	// Use this for initialization
	void Start () {
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		IsFollowing = true;
	}
	
	// Update is called once per frame
	/*void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothLineX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothLineY);

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}*/

	void Update(){
		var x = transform.position.x;
		var y = transform.position.y;

		if (IsFollowing) {
			if (Mathf.Abs (x - player1.position.x) > margin.x)
				x = Mathf.Lerp (x, player1.position.x, smoothing.x * Time.deltaTime);
			if (Mathf.Abs (y - player1.position.y) > margin.y)
				y = Mathf.Lerp (y, player1.position.y, smoothing.y * Time.deltaTime);
		}

		var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

		x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		y = Mathf.Clamp (y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);

		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
