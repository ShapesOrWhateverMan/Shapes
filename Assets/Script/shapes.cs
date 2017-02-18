using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class shapes : MonoBehaviour {

	private Rigidbody2D rigBody;
	private float inputHoriz;
	private Animator anim;
	private float speed;
	private bool facingRight = true;
	private float jumpStrength = 2;
	private bool canJump = true;
	public Transform startPos;
	public LayerMask groundLayer;
	private bool grounded;
	private float groundRadius = 0.59f;
	private bool goodOrBad; //True means good
	private float abilityPoints;
	private List<int> relations;
	private int charPos;
	private bool alive;
	public int maxHealth = 100;
	public int health { get; private set; }
	public GameObject ouchEffect;
	public bool isDead;
	//private CharacterController2D controller;

	//This is the ability function
	//THis is right here is just a space taker
	//The actual function will be implemented in the
	//player's own script
	public virtual void ability(){}

	//Setters
	public void isGoodOrBad(bool v) { goodOrBad = v; /*True means good*/}
	public void setAP(float s) { abilityPoints = s;}
	public void setCharPos(int c) { charPos = c; }
	public void setGrounded(bool s) { grounded = s; }
	public void setRigBody2D(Vector2 v) {rigBody.velocity = v; }

	//Getters
	public bool checkIfGood() { return goodOrBad; } //Check if a character is good or not (getter for goodOrBad)
	public float getAbilityPoints() { return abilityPoints; }
	public int getCharPos() { return charPos; }
	public Rigidbody2D getRigidBody2D() { return rigBody; }


	//Getter for currentCharPos

	// Use this for initialization
	public void Awake () {
		isDead = false;
		health = maxHealth;
		grounded = false;
		relations = new List<int>(10);

		//Initialization of the list

		speed = 2;
		anim = gameObject.GetComponent<Animator> ();
		//You are passing the character controller (the one in unity)
		//to the controller above for reference
		//controller = GetComponent<CharacterController> ();
		rigBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		//Checking if the character is grounded
		//RaycastHit2D hit = Physics2D.Linecast(startPos.position, endPos.position, groundLayer.value);
		//grounded = Physics2D.OverlapCircle(startPos.position, groundRadius, groundLayer);
		//anim.SetBool ("Grounded", grounded);
		//anim.SetFloat ("Speed", rigBody.velocity.y);

		inputHoriz = Input.GetAxisRaw ("Horizontal");
		//This is where the jump animation is told to play

		anim.SetFloat("xSpeed", Mathf.Abs(inputHoriz));

		rigBody.velocity = new Vector2 (inputHoriz * speed, rigBody.velocity.y);

		//if moving left then flip the player around so the animation will play in the correct
		//direction
		if (inputHoriz < 0 && facingRight) {
			flip();
		} //Now to face back right if the right key is pressed
		else if (inputHoriz > 0 && !facingRight) {
			flip();
		}

		if (grounded) {
			canJump = true;
		} else {
			Debug.Log ("grounded is false");
		}

		//Now for the abilities
		if (Input.GetKeyDown (KeyCode.F)) {
			ability ();
		}

	}

	void Update(){
		if (!isDead)
			HandleInput ();
	}

	void HandleInput(){
		//Now for the jumping
		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			//Also the the code for changing the boolean for the 
			//jump in UNity like in the video (for the animation to play)
			anim.SetBool ("Grounded", grounded);
			canJump = false;
			rigBody.velocity = new Vector2 (rigBody.velocity.x, jumpStrength);
		}
	}

	//Flips the around 180 from left to right or right to left
	void flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	// Use this for initialization
	void OnCollisionStay2D (Collision2D coll) {
		if(coll.gameObject.tag == "Ground")
			grounded = true;
		if (coll.gameObject.tag == "Spring")
			rigBody.velocity = new Vector2 (rigBody.velocity.x, 5);
	}

	void OnCollisionExit2D(Collision2D coll){
		grounded = false;
	}

	//************************Violence*************/// <summary>
	/// Below are the methods for damage killing and spawning
	/// </summary>
	public void kill()
	{
		//GetComponent<CharacterController2D> ().HandleCollisions = false;
		GetComponent<Collider2D> ().enabled = false;
		isDead = true;
		health = 0;
	}

	public void RespawnAt(Transform spawnPoint)
	{
		if (!facingRight)
			flip ();

		isDead = false;
		GetComponent<Collider2D> ().enabled = true;
		//GetComponent<CharacterController2D> ().HandleCollisions = true;

		transform.position = spawnPoint.position;

		health = maxHealth;
	}

	//When the player takes damage
	public void takeDamage(int damage){
		Instantiate (ouchEffect, transform.position, transform.rotation);
		health -= damage;

		if (health <= 0) {
			levelManage.instance.killPlayer ();
		}
	}

}
