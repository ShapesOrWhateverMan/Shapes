using UnityEngine;

public class giveDamageToPlayer : MonoBehaviour {

	public int DamageToGive = 10;
	private Vector2
		lastPosition,
		velocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		velocity = (lastPosition - (Vector2) transform.position) / Time.deltaTime;
		lastPosition = transform.position;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		var player = other.GetComponent<shapes>();

		if (player == null)
			return;
		
		player.takeDamage (DamageToGive);

		var controller = player.GetComponent<Rigidbody2D> ();
		var totalVel = controller.velocity + velocity;

		controller.AddForce (new Vector2(
			-1 * Mathf.Sign(totalVel.x) * Mathf.Clamp(Mathf.Abs(totalVel.x) * 5, 10, 20),
			-1 * Mathf.Sign(totalVel.y) * Mathf.Clamp(Mathf.Abs(totalVel.y) * 2, 0, 15)));

	}
}
