using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBird : MonoBehaviour {

    Animator anim;
    public float distance = 5f;
    float initX;
    float leftBoundaryX = -5f;
    float rightBoundaryX = 5f;
    public int xdir = 0;
    int xdire = 0;
    public float speed = 1f;
    float x = 0;
    public int playerHitBoxLayerID = 11;
    public float damage = 20f;

    // Use this for initialization
    void Start () {
        initX = transform.position.x;
        float maxBoundary = initX + (distance * xdir);
        if (maxBoundary > initX)
        {
            rightBoundaryX = maxBoundary;
            leftBoundaryX = initX;
        }
        else {
            rightBoundaryX = initX;
            leftBoundaryX = maxBoundary;
        }
        if (xdire == 0) xdire = 1;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= leftBoundaryX)
        {
            xdire = 1;
        }
        else if (transform.position.x >= rightBoundaryX)
        {
            xdire = -1;
        }
        anim.SetInteger("xdir", xdire);
        transform.position.Set(Mathf.Clamp(transform.position.x, leftBoundaryX, rightBoundaryX), transform.position.y, transform.position.z);
        transform.position += new Vector3(Mathf.Clamp(distanceToBoundary(),-speed*Time.deltaTime,speed*Time.deltaTime),0f,0f);
    }

    float distanceToBoundary() {
        if (xdire >= 1) {
            return Mathf.Abs(rightBoundaryX - transform.position.x);
        } else {
            return -Mathf.Abs(leftBoundaryX - transform.position.x);
        }
    }

    void onTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == playerHitBoxLayerID) {
            PlayerHealth.health -= damage;
        }
    }
}
