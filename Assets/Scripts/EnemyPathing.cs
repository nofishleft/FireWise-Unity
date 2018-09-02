using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    bool enemySpotted = false;

    // put the points from unity interface
    public Transform[] wayPointList;


    Animator anim;

    //public float fov = 30f;
    public float visionDistance = 0.05f;
    public int currentWayPoint = 0;
    Transform targetWayPoint;
    PlayerMovement trackedPlayer;
    Vector3 lastSeenLocation;
    public int playerHitboxLayerID = 11;
    public int playerLayerID = 10;
    public float knockbackSpeed = 0.01f;
    public float damage = 20f;

    float xdir;
    float ydir;

    public float speed = 0.4f;
    public float rotationalSpeed = 50f;
    public float pathSpeed = 1f;

    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        trackedPlayer = PlayerMovement.player;
        // check if we have somewere to walk
        walkEnemy();
        anim.SetFloat("xdir", xdir);
        anim.SetFloat("ydir", ydir);
        anim.SetFloat("speed", speed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && col.gameObject.layer == playerLayerID)
        {
            PlayerMovement p = col.gameObject.GetComponent<PlayerMovement>();
            Vector3 vectorToTarget = p.transform.position - transform.position;
            Vector3 v = knockbackSpeed * Vector3.Normalize(vectorToTarget);
            if (PlayerHealth.health >= 0f) PlayerHealth.health -= damage;
            GetComponent<Rigidbody2D>().velocity = -v;
        }
    }

    bool enemyStillThere() {
        RaycastHit2D hit = Physics2D.Linecast(transform.position,trackedPlayer.transform.position);
        if (hit.collider != null && hit.collider.gameObject.layer == playerHitboxLayerID) {
            lastSeenLocation = trackedPlayer.transform.position;
            return true;
        }
        return false;
        
    }

    void walkEnemy() {
        if (!enemySpotted) {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            Vector3 vectorToPlayer = PlayerMovement.player.transform.position - transform.position;
            if ((float) vectorToPlayer.sqrMagnitude <= (float) visionDistance * visionDistance) {
                Debug.Log("test");
                RaycastHit2D hit = Physics2D.Linecast(transform.position, trackedPlayer.transform.position);
                if (hit.collider != null && hit.collider.gameObject.layer == playerHitboxLayerID) enemySpotted = true;
                Debug.Log(transform.position);
                Debug.Log(trackedPlayer.transform.position);
                Debug.Log(hit);
            }
        }
        if (!enemySpotted) {
            if (currentWayPoint < this.wayPointList.Length)
            {
                walk();
            }
        } else {
            if (enemyStillThere())
            {
                Vector3 vectorToTarget = trackedPlayer.transform.position - transform.position;
                move(vectorToTarget);
            }
            else
            {
                if (transform.position != lastSeenLocation)
                {
                    Vector3 vectorToTarget = lastSeenLocation - transform.position;
                    move(vectorToTarget);
                }
            }
        }                
    }
    
    void move(Vector3 vectorToTarget)
    {
        float x = Mathf.Abs(vectorToTarget.x);
        float newx = 0f;
        float y = Mathf.Abs(vectorToTarget.y);
        float newy = 0f;
        if (x > y)
        {
            newx += Mathf.Clamp((vectorToTarget.x / x) * speed * Time.deltaTime, -x, x);
            xdir = newx/Mathf.Abs(newx);
            ydir = 0f;
        }
        else
        {
            newy += Mathf.Clamp((vectorToTarget.y / y) * speed * Time.deltaTime, -y, y);
            ydir = newy/Mathf.Abs(newy);
            xdir = 0f;
        }
        transform.position += new Vector3(newx, newy);
    }

    void moveDiag(Vector3 vectorToTarget)
    {
        float x = Mathf.Abs(vectorToTarget.x);
        float newx = 0f;
        float y = Mathf.Abs(vectorToTarget.y);
        float newy = 0f;
        if (x > y)
        {
            newx += Mathf.Clamp((vectorToTarget.x / x) * pathSpeed * Time.deltaTime, -x, x);
            xdir = newx / Mathf.Abs(newx);
            ydir = 0f;
        }
        else
        {
            newy += Mathf.Clamp((vectorToTarget.y / y) * pathSpeed * Time.deltaTime, -y, y);
            ydir = newy / Mathf.Abs(newy);
            xdir = 0f;
        }
        Vector3 dir = Vector3.Normalize(vectorToTarget);
        float sp = Mathf.Clamp(vectorToTarget.magnitude,-pathSpeed*Time.deltaTime,pathSpeed*Time.deltaTime);
        transform.position += sp * dir;
    }

    void walk()
    {
        moveDiag(targetWayPoint.position - transform.position);
        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            if (wayPointList.Length == currentWayPoint) currentWayPoint = 0;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
