using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
    public Rigidbody ball;
    public Rigidbody board;


    public bool isCollide = false;

    public float x;
    public float y;

    private float ballVelocity = 300;
	
	// Update is called once per frame
	void Update () {
        x = ball.velocity.x;
        y = ball.velocity.y;
        if (isCollide)
        {
            Vector3 v = calVelocity();
            v.x *= 100000;
            v.y *= 100000;
            ball.velocity = v;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(ball);
        isCollide = true;
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
