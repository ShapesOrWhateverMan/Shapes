using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {
    //Target to chase
    public Transform target;

    //How many times per second do we update the path
    public float repathRate = 2f;
    protected float lastRepath = -9999;

    // Caching
    protected Seeker seeker;
    protected Rigidbody2D rb;

    //calculated path
    public Path path; 

    //AI speed per second
    public float speed = 300f;
    
    //Max distance from a waypoint before continuing to the next waypoint
    public float nextWaypointDistance = 3;

    //Waypoint we are currently moving towards
    protected int currentWaypoint = 0;

    protected bool searchingForPlayer = false;

    [HideInInspector]
    public bool pathIsEnded = false;

    public virtual void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator SearchForPlayer() {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if(sResult == null){
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }else{
            searchingForPlayer = false;
            target = sResult.transform;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    public IEnumerator UpdatePath() {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / repathRate);
        StartCoroutine(UpdatePath());
    }   

    public void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    public virtual void FixedUpdate () {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        if (path == null) {
            return;
        }
        //TODO: Always face towards player
        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathIsEnded)
                return;
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
}
