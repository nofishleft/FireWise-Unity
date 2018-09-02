﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speedInit = 2.2f;
    public static float speedMax = 2.2f; // Max speed. I don't know why this doesn't show up in the editor.
    public static float speedMin = 0.8f;
    public float acceleration = 0.5f;
    public float deceleration = 0.25f;

    public static float speed;
    string lastKey = "w";
    static KeyCode[] strafekeys = new KeyCode[] { KeyCode.A, KeyCode.D };
    static KeyCode[] walkkeys = new KeyCode[] { KeyCode.W, KeyCode.S };

    public static int xdir = 0;
    public static int ydir = 0;

	// Use this for initialization
	void Start () {
		speed = speedInit;		
	}

        // Detect if user is pressing both A&D or W&S (opposites)
        bool opposites = false;
        if (Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("a") && Input.GetKey("d")) {
            opposites = true;
        }

		// If the user is holding down the same key as the last time, we should increase their speed
		if (Input.GetKey(lastKey) && !opposites) {
            Debug.Log(opposites);
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

		transform.Translate(x, y, 0);

        // Debug.Log(speed);
	}
}
