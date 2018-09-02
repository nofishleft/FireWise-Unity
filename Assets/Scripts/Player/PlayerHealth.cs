using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public static float healthMax = 100;
    public float healthDecay = 1f; // How much health the player will lose per second
    public float moveFactor = 0.5f; // How much decay will be reduced by when the player is travelling at their max speed

    public static float health;

    // Use this for initialization
    void Start () {
        health = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
        float decay;
        float speedPercent;

        // Determine how much of the player's potential speed they are travelling
        speedPercent = (PlayerMovement.speed - PlayerMovement.speedMin) / (PlayerMovement.speedMax - PlayerMovement.speedMin);

        // Calculate the net decay after taking into account the player's movement speed
        decay = healthDecay * (1 - moveFactor * speedPercent);

        health -= decay * Time.deltaTime;

	}
}
