using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speedInit = 3f;
	public static float speedMax = 8f; // Max speed. I don't know why this doesn't show up in the editor.
	public static float speedMin = 2f;
	public float acceleration = 0.5f;
	public float deceleration = 0.25f;
	
	public static float speed;
	string lastKey = "w";

	// Use this for initialization
	void Start () {
		speed = speedInit;		
	}
	
	// Update is called once per frame
	void Update () {
        float x = 0;
        float y = 0;

		// If the user is holding down the same key as the last time, we should increase their speed
		if (Input.GetKey(lastKey)) {
			speed += acceleration * Time.deltaTime;
		} else {
			speed -= deceleration * Time.deltaTime;
		}

		// The player's speed should never go over speedMax or under speedMin
		if (speed > speedMax) {
			speed = speedMax;
		} else if (speed < speedMin) {
			speed = speedMin;
		}

        if (Input.GetKey("w"))
        {
            y = 1;
            lastKey = "w";
        }
        else if (Input.GetKey("a"))
        {
            x = -1;
            lastKey = "a";
        }
        else if (Input.GetKey("s"))
        {
            y = -1;
            lastKey = "s";
        }
        else if (Input.GetKey("d"))
        {
            x = 1;
            lastKey = "d";
        }

        // Basic movement code with slight adjustments
        // From: https://unity3d.com/learn/tutorials/topics/multiplayer-networking/creating-player-movement-single-player
        x = x * Time.deltaTime * speed;
		y = y * Time.deltaTime * speed;

		transform.Translate(x, y, 0);

        Debug.Log(speed);
	}
}
