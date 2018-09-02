using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

    public int playerLayerID = 10;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void onTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hello");
        if (col.gameObject.layer == playerLayerID)
        {
            col.gameObject.GetComponent<Animator>().SetBool("fire", false);
        }
    }
}
