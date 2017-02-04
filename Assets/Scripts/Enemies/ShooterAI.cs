using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class ShooterAI : EnemyAI {
    [HideInInspector]
    public bool pathIsEnded = false;

    public void Start() {
        seeker = GetComponent<Seeker>();
        StartCoroutine(UpdatePath());
    }

    public void FixedUpdate() {
        if (target == null || path == null) {
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
