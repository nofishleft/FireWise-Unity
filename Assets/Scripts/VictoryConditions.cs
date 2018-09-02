using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be placed on a gameobject with a box collider 2d designating the target area
// for the player to reach.
public class VictoryConditions : MonoBehaviour {

    public int playerLayerID = 10; // Layer ID of the player

    // If the player reaches the target area in a scene, they have won
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision with: " + col.gameObject.name);
        if (col.gameObject.layer == playerLayerID)
        {
            Debug.Log("Player Victory");
            GameManager.instance.Victory();
        }
    }
}
