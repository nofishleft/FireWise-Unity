﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This should be placed on a gameobject with a box collider 2d designating the target area
// for the player to reach. This will cause the player to play a short animation before while fading to black.
public class GameVictory : MonoBehaviour
{
    public float fadePanelDuration = 5.5f;
    public float fadeTextDuration = 3.5f;
    public Image fadePanel; // A panel that will gradually fade in from alpha 1 to alpha 255;
    public Text fadeText; // Text that will gradually fade in from alpha 1 to alpha 255
    public int playerLayerID = 10; // Layer ID of the player

    float timeToWait = 0f;

    private void Start()
    {
        timeToWait = Mathf.Max(fadePanelDuration, fadeTextDuration);
    }

    // If the player reaches the target area in a scene, they have won
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision with: " + col.gameObject.name);
        if (col.gameObject.layer == playerLayerID)
        {
            Debug.Log("Player Victory");
            fadeText.CrossFadeAlpha(255, fadeTextDuration, false);
            fadePanel.CrossFadeAlpha(255, fadePanelDuration, false);
            if (timeToWait > 0)
            {
                timeToWait -= Time.deltaTime;
                return;
            } else
            {
                GameManager.instance.Victory();
            } 
        }
    }
}