using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speedInit = 2.2f;
    public static float speedMax = 0.7f; // Max speed. I don't know why this doesn't show up in the editor.
    public static float speedMin = 0.3f;
    public float acceleration = 0.5f;
    public float deceleration = 0.25f;

    public static PlayerMovement player;


    // These two variables are used by beartraps to record how long and how much to slow the player by
    public static float slowDuration = 0f;
    public static float slowFlat = 0f;
    public static float slowPercent = 0f;

    public static float speed;
    string lastKey = "w";

    public static int xdir = 0;
    public static int ydir = 0;

    public int wallID = 0; // Layer number of the walls that the player should bounce off
    public float bounceDmg = 10;

    // If the player collides with a wall, reverse their vector and set their speed to the minimum
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == wallID)
            // Debug.Log("Player collision");
        {
            xdir *= -1;
            ydir *= -1;
            speed = speedMin;
            PlayerHealth.health -= bounceDmg;
        }
    }

    // Use this for initialization
    void Start () {
		speed = speedInit;
        player = this;
	}

    private void Update()
    {

    // Detect if user is pressing both A&D or W&S (opposites)
    bool opposites = false;
        if (Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("a") && Input.GetKey("d")) {
            opposites = true;
        }

		// If the user is holding down the same key as the last time, we should increase their speed
		if (Input.GetKey(lastKey) && !opposites) {
            // Debug.Log(opposites);
            //Debug.Log("Accelerating");
            speed += acceleration * Time.deltaTime;
		} else if(Input.anyKey) {
            speed = speedMin;
        } else {
            //Debug.Log("Decelerating");
            speed -= deceleration * Time.deltaTime;
        }

        // The player's speed should never go over speedMax or under speedMin
        if (PlayerHealth.health > 0f) {
            speed = Mathf.Clamp(speed, speedMin, speedMax);
        } else {
            speed = 0f;
        }

        if (Input.GetKey("w"))
        {
            ydir = 1;
            xdir = 0;
            lastKey = "w";
        }
        else if (Input.GetKey("a"))
        {
            ydir = 0;
            xdir = -1;
            lastKey = "a";
        }
        else if (Input.GetKey("s"))
        {
            ydir = -1;
            xdir = 0;
            lastKey = "s";
        }
        else if (Input.GetKey("d"))
        {
            ydir = 0;
            xdir = 1;
            lastKey = "d";
        }

        // Basic movement code with slight adjustments
        // From: https://unity3d.com/learn/tutorials/topics/multiplayer-networking/creating-player-movement-single-player
        var x = xdir * Time.deltaTime * speed;
		var y = ydir * Time.deltaTime * speed;

        // Adjust the player's speed if they've walked into a bear trap
        if (slowDuration > 0)
        {
            x = x * (1 - slowPercent) - slowFlat;
            y = y * (1 - slowPercent) - slowFlat;
            slowDuration -= Time.deltaTime;
        } else {
            slowPercent = 0f;
            slowFlat = 0f;
        }

		transform.Translate(x, y, 0);

        // Debug.Log(speed);
	}
}
