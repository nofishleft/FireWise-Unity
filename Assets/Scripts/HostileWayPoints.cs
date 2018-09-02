using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileWayPoints : MonoBehaviour {

    // put the points from unity interface
    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;


    public int playerLayerID = 10;
    public float speed = 1f;
    public float knockbackSpeed = 4f;
    public bool rotate= true;
    public float damage = 20f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length) {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && col.gameObject.layer == playerLayerID)
        {
            Vector3 v = knockbackSpeed * Vector3.Normalize(transform.forward);
            if (PlayerHealth.health >= 0f) PlayerHealth.health -= damage;
            PlayerMovement.player.GetComponent<Rigidbody2D>().velocity = knockbackSpeed * Vector3.Normalize(transform.forward);
            GetComponent<Rigidbody2D>().velocity = -v;
        }
    }

    void walk() {
        // rotate towards the target
        if (rotate) {
            Vector3 targetdiff = targetWayPoint.position - transform.position;
            targetdiff.x = transform.forward.x;
            targetdiff.y = transform.forward.y;
            transform.forward = Vector3.RotateTowards(transform.forward, targetdiff, speed * Time.deltaTime, 0.0f);
        }
        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position) {
            currentWayPoint++;
            if (wayPointList.Length == currentWayPoint) currentWayPoint = 0;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
