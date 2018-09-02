using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalc_UI : MonoBehaviour {

    public GameObject scoreIndicator;
    Text scoreText;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        float scorePercent = 1 - (PlayerHealth.healthMax - PlayerHealth.health) / PlayerHealth.healthMax;

        // Move HealthIndicator from 90 degress to -90 degrees as their health goes down
        var score = (System.Math.Round(900 * scorePercent)*10).ToString().PadLeft(4,'0');
        scoreText.text = score;
    }
}
