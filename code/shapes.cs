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
	public Transform endPos;
	public LayerMask groundLayer;
	private bool grounded = false;
	private float groundRadius = 0.2f;
	private bool goodOrBad; //True means good
	private float healthPoints;
	private float abilityPoints;
	private List <int> relations;
	private int charPos;


	//This is the ability function
	//THis is right here is just a space taker
	//The actual function will be implemented in the
	//player's own script
	public virtual void ability(){}

	//True setter for goodOrBad
	public void isGoodOrBad(bool v){
		goodOrBad = v; //True means good
	}

	//Check if a character is good or not (getter for goodOrBad)
	public bool checkIfGood(){
		return goodOrBad;
	}

	//Getter for the healthPoints
	public float getHealthPoints(){
		return healthPoints;
	}

	//Getter for the AbilityPOints
	public float getAbilityPoints(){
		return abilityPoints;
	}

	//Setter for the healthPoints
	public void setHP(float s){
		healthPoints = s;
	}

	//Setter for the abilityPOints
	public void setAP(float s){
		abilityPoints = s;
	}

	//Setter for currentCharPos
	public void setCharPos(int c){
		charPos = c;
	}

	//Getter for currentCharPos
	public int getCharPos(){
		return charPos;
	}

	// Use this for initialization
	public void Start () {
		Debug.Log ("In the start ");

		relations = new List<int>(10);

		//Initialization of the list

		speed = 2;
		anim = GetComponent<Animator> ();
		//You are passing the character controller (the one in unity)
		//to the controller above for reference
		//controller = GetComponent<CharacterController> ();
		rigBody = this.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		//Checking if the character is grounded
		//RaycastHit2D hit = Physics2D.Linecast(startPos.position, endPos.position, groundLayer.value);
		grounded = Physics2D.OverlapCircle(startPos.position, groundRadius, groundLayer);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rigBody.velocity.y);

		inputHoriz = Input.GetAxisRaw ("Horizontal");
		//This is where the jump animation is told to play

		anim.SetFloat("Speed", Mathf.Abs(inputHoriz));

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
		}

		//Now for the abilities
		if (Input.GetKeyDown (KeyCode.F)) {
			ability ();
		}

	}

	void Update(){
		//Now for the jumping
		if (Input.GetKeyDown(KeyCode.Space) && canJump) {
			//Also the the code for changing the boolean for the 
			//jump in UNity like in the video (for the animation to play)
			anim.SetBool("Ground", false);
			canJump = false;
			rigBody.velocity = new Vector2 (rigBody.velocity.x, jumpStrength);
			//rigBody.AddForce(new Vector2(0, jumpStrength));
			grounded = false;
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
}
