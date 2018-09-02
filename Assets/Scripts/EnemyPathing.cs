﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    bool enemySpotted = false;

    // put the points from unity interface
    public Transform[] wayPointList;

    public float fov = 30f;
    public float visionDistance = 1f;
    public int currentWayPoint = 0;
    Transform targetWayPoint;
    PlayerMovement trackedPlayer;
    Vector3 lastSeenLocation;
    public int playerLayerID = 10;
    public float damageSpeed = 0.5f;

    public float speed = 4f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        PlayerMovement p = PlayerMovement.player;
        Vector3 separationVector = p.transform.position - transform.position;
        separationVector.z = 0;
        if (separationVector.sqrMagnitude <= visionDistance * visionDistance) {
            Vector3 forwardVector = transform.forward;
            float ang = Vector3.Angle(forwardVector, separationVector);
            if (ang <= fov / 2) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(forwardVector.x,forwardVector.y));
                if (hit.collider != null && hit.collider.gameObject.layer == playerLayerID) {
                    trackedPlayer = p;
                    lastSeenLocation = trackedPlayer.transform.position;
                    lastSeenLocation.z = transform.position.z;
                    enemySpotted = true;
                }
            }
        }

        // check if we have somewere to walk
        if (enemySpotted) {
            walkEnemy();
        } else if (currentWayPoint < this.wayPointList.Length) {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walkPath();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == playerLayerID) {
            Vector3 v = damageSpeed * Vector3.Normalize(transform.forward);
            PlayerHealth.health -= 0.5f * PlayerHealth.healthMax;
            trackedPlayer.GetComponent<Rigidbody2D>().velocity = damageSpeed*Vector3.Normalize(transform.forward);
            GetComponent<Rigidbody2D>().velocity = -v;
        }
    }

    bool enemyStillThere() {
        Vector3 separationVector = trackedPlayer.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(separationVector.x, separationVector.y), visionDistance);
        if (hit.collider != null && hit.collider.gameObject.layer == playerLayerID) return true;
        return false;
        
    }

    void walkEnemy() {
        // rotate towards the target
        Vector3 rot = Vector3.RotateTowards(transform.forward, lastSeenLocation - transform.position, speed * Time.deltaTime, 0.0f);
        transform.forward = rot;
        // move towards the target
        Vector3 tra = Vector3.MoveTowards(transform.position, lastSeenLocation, speed * Time.deltaTime);
        transform.position = tra;

        if (transform.position == lastSeenLocation)
        {
            if (enemyStillThere()) {
                lastSeenLocation = trackedPlayer.transform.position;
                lastSeenLocation.z = transform.position.z;
                transform.forward = Vector3.RotateTowards(transform.forward, lastSeenLocation, Vector3.Angle(transform.forward, lastSeenLocation), 0.0f);
            }
        }
    }

    void walkPath() {
            // rotate towards the target
            Vector3 rot = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);
            transform.forward = rot;

            // move towards the target
            Vector3 tra = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);
            transform.position = tra;

            if (transform.position == targetWayPoint.position)
            {
                currentWayPoint++;
                if (wayPointList.Length == currentWayPoint) currentWayPoint = 0;
                targetWayPoint = wayPointList[currentWayPoint];
            }
    }
}
