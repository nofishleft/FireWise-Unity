using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    public GameObject healthIndicator;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        float healthPercent = (PlayerHealth.healthMax - PlayerHealth.health) / PlayerHealth.healthMax;

        // Move HealthIndicator from 90 degress to -90 degrees as their health goes down
        var z = 90 - 180 * healthPercent;

        Quaternion target = Quaternion.Euler(0, 0, z);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);


    }
}
