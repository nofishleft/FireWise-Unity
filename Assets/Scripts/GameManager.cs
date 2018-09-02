using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static GameObject[] scenes;

    private void Awake()
    {
        // If a GameManager hasn't been instantiated yet, set the latest one as the only instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else if (instance != null)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
