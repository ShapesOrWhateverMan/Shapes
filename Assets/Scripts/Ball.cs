using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float ballVelocity = 300;
    public Rigidbody rb;
    public Transform board;
    private bool isPlay = false;

    public float resetPosVel = 50;

    // Use this for initialization
    void Awake () {
        rb = gameObject.GetComponent<Rigidbody>();
       
	}
	
	// Update is called once per frame
	void Update () {

	    if(Input.GetMouseButton(0) == true && !isPlay)
        {
            transform.parent = null;
            isPlay = true;
            rb.isKinematic = false;
            rb.AddForce(calVelocity());
        }
    }


    Vector3 calVelocity()
    {
        float deltaY = gameObject.transform.position.y - board.transform.position.y;
        float deltaX = gameObject.transform.position.x - board.transform.position.x;
        float hyp = Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2));
        float xVel = ballVelocity * deltaX / hyp;
        float yVel = ballVelocity * deltaY / hyp;

        return new Vector3(xVel, yVel, 0);
    }
}
