using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AI : Enemy {
    //For Animation
    private SpriteRenderer spriteRenderer;

    //Status
    public bool isAlive = true;

    public Transform target;
    public float iceShardCooldown;
    public float iceBallCooldown;
    public float iceSpikeCooldown;

    public float triggerRadius = 30f;
    private float nextTimeToSearch = 0;
    //public int fireRotationOffset = 90;
    public LayerMask hitLayer;

    //Attack cycle: changes every 30% missing health
    protected double[] thresholds;
    protected int attackCycle = 0;
    private bool criticalMode = false;  //Down to 10% health

    //Projecile spawn points
    List<Transform> shardPoints;    //Ice Shards
    Transform firePoint;            //Ice Ball
    //List<Transform> spikeStartPoints;   //Ice Spikes
    Transform spikeStartPoint;

    //Number of shard numbers
    private int numShardPts;

    //Next time to use ability
    private float timeToFireShard = 3;
    private float timeToFireBall = 0;
    private float timeToSpawnSpikes = 0;

    //The boss's positions in the battlefield
    Transform[] positions;

    //Movement speed
    private float moveSpeed;

    //Next time to move to a new position
    private float nextTimeToMove;
    private float repositionTime;
    private int currPos;
    private bool isMoving;

    //Transform firePoint;
    public Transform IceShard;
    public Transform IceBallPrefab;
    public Transform IceSpikesPrefab;

    void Awake() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //Stats
        stats.maxHealth = 1000f;
        stats.health = stats.maxHealth;

        //Set attack cycle health tresholds
        thresholds = new double[3];
        thresholds[2] = 0.7 * stats.maxHealth;
        thresholds[1] = 0.4 * stats.maxHealth;
        thresholds[0] = 0.1 * stats.maxHealth;

        //PROJECTILE FIREPOINTS
        //Ice Shards
        shardPoints = new List<Transform>();
        numShardPts = 0;
        for (int i = 1; i <= 8; i++) {
            Transform s = transform.FindChild("ShardPoint" + i);
            if (s == null)
                Debug.LogError("Shardpoint " + i + " is null");
            else {
                numShardPts++;
                shardPoints.Add(s);
            }
        }
        //Ice Ball
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("Firepoint is null");
        }
        //Ice Spikes
        Transform spikePoint1 = GameObject.Find("IceSpikesSpawnPoint1").GetComponent<Transform>();
        //Transform spikePoint2 = GameObject.Find("IceSpikesSpawnPoint2").GetComponent<Transform>();
        /* if (spikePoint1 == null || spikePoint2 == null) {
            Debug.LogError("A spike spawn point is null");
        }
        else {
            spikeStartPoints = new List<Transform>();
            spikeStartPoints.Add(spikePoint1);
            spikeStartPoints.Add(spikePoint2);
        }*/
        if (spikePoint1 == null) {
            Debug.LogError("A spike spawn point is null");
        }
        else {
            spikeStartPoint = spikePoint1;
        }

        //POSITIONS
        positions = new Transform[6];
        for (int i = 0; i < 6; i++) {
            Transform p = GameObject.Find("StopPoint" + (i + 1)).GetComponent<Transform>();
            if (p == null)
                Debug.LogError("StopPoint " + (i+1) + " not found");
            else {
                positions[i] = p;
            }
        }

        //Cooldowns
        iceShardCooldown = 4;
        iceBallCooldown = 14;
        iceSpikeCooldown = 12;

        //Movement time
        repositionTime = 8f;
        nextTimeToMove = Time.time + repositionTime;
        currPos = 0;
        isMoving = false;
        moveSpeed = 30f;
    }

    // Update is called once per frame
    void Update() {
        if (!isAlive)
            return;

        if (target == null) {
            FindPlayer();
            return;
        }

        //Update attack cycle
        if (stats.health < thresholds[0])
            attackCycle = 3;
        else if (stats.health < thresholds[1])
            attackCycle = 2;
        else if (stats.health < thresholds[2])
            attackCycle = 1;
        else
            attackCycle = 0;

        //Always face the player (BUG: does not go with ChangePosition())
        //AimAtTarget();

        //Increase aggressiveness & agility when at low HP
        //HP below 40%
        if (attackCycle >= 2)
            repositionTime = 6f;

        //HP below 10%
        if (!criticalMode && attackCycle == 3) {
            criticalMode = true;
            iceShardCooldown = 2.5f;
            iceBallCooldown = 6.5f;
            iceSpikeCooldown = 6f;
            repositionTime = 4f;
            //Critical Animation placeholder
            spriteRenderer.color = new Color(255f, 0f, 0f);
        }

        //MOVEMENT
        ChangePosition();

        //ATTACK
        //Fire Shards
        if (Time.time > timeToFireShard) {
            timeToFireShard = Time.time + iceShardCooldown;
            IceShards();
        }

        //Spawn Ice Spikes starting from HP < 70%
        if (attackCycle >= 1 && Time.time > timeToSpawnSpikes) {
            timeToSpawnSpikes = Time.time + iceSpikeCooldown;
            IceSpikes();
        }

        //Fire IceBall starting from HP < 40%
        if (attackCycle >= 2 && Time.time > timeToFireBall) {
            timeToFireBall = Time.time + iceBallCooldown;
            IceBall();
        }
    }
    
    //MOVEMENT
    void ChangePosition(){
        if (Time.time > nextTimeToMove) {
            //Random Position
            //Randomly select a position from predetermined set of positions
            int nextPos = Random.Range(0, positions.Length);
            while (nextPos == currPos) {
                nextPos = Random.Range(0, positions.Length);
            }
            currPos = nextPos;
            isMoving = true;
            nextTimeToMove = Time.time + repositionTime;
        }

        //Actual Movement
        if (isMoving) {
            //Translate to the chosen position
            Vector3 difference = positions[currPos].position - transform.position;
            //Stop if close enough
            if (difference.magnitude <= 0.5)
                isMoving = false;
            else {
                difference.Normalize();
                transform.Translate(difference * Time.deltaTime * moveSpeed);
            }
        }
    }

    //PROJECTILES
    void IceBall() {
        Instantiate(IceBallPrefab, firePoint.position, firePoint.rotation);
    }

    void IceShards() {
        if (attackCycle == 0)
            StartCoroutine(SpawnShards(3));
        else if (attackCycle == 1)
            StartCoroutine(SpawnShards(5));
        else if (attackCycle == 2)
            StartCoroutine(SpawnShards(8));
        else {
            StartCoroutine(SpawnShards(8));
        }

    }

    void IceSpikes() {
        StartCoroutine(SpawnSpikes(30));
    }

    IEnumerator SpawnSpikes(int numSpikes) {
        float posShift = 0.5f;        //TODO: set to width of ice spike prefab
        /*int ipos = Random.Range(0, 2);
        if (ipos == 1)
            posShift *= -1; //Spikes "move" to the left
        */
        float x = spikeStartPoint.position.x;
        float y = spikeStartPoint.position.y;
        Quaternion rot = spikeStartPoint.rotation;
        //Instantiate spikes
        for (int i = 0; i < numSpikes; i++) {
            if (!isAlive) break;
            Instantiate(IceSpikesPrefab, new Vector3(x + (posShift * i), y), rot);
            Instantiate(IceSpikesPrefab, new Vector3(x - (posShift * i), y), rot);
            //Inversely proportional to "horizontal" velocity
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SpawnShards(int numShards) {
        if (numShards == 8) {
            foreach (Transform point in shardPoints) {
                if (!isAlive) break;
                Instantiate(IceShard, point.position, point.rotation);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else {
            for (int i = 0; i < numShards; i++) {
                if (!isAlive) break;
                int spawnPointIndex = (i * numShards) % numShardPts;
                Instantiate(IceShard, shardPoints[spawnPointIndex].position, shardPoints[spawnPointIndex].rotation);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    //TARGETING
    void AimAtTarget() {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void FindPlayer() {
        if (nextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null) {
                target = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }

    //Death
    void DamageEnemy(float damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            //Leave the corpse, don't destroy it
            DeathState();
        }
    }

    void DeathState() {
        isAlive = false;
        spriteRenderer.color = new Color(255f, 255f, 255f);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject p in projectiles) {
            p.SendMessage("DamageEnemy", Mathf.Infinity, SendMessageOptions.DontRequireReceiver);
        }
    }
    //ANIMATION
}
