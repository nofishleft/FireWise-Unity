using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatConditions : MonoBehaviour {

    public float defeatDelay = 3;
    bool waiting = false;
    float timeToWait = 0f;

    // Update is called once per frame
    void Update () {
		
        if (PlayerHealth.health < 0)
        {
            Debug.Log("Player dead");
            if (waiting)
            {
                Debug.Log("Player defeated");
                timeToWait -= Time.deltaTime;
                if (timeToWait < defeatDelay)
                {
                    waiting = false;
                    GameManager.instance.Defeat();
                }
            }  else
            {
                waiting = true;
                timeToWait = defeatDelay;
            }
        }
	}
}
