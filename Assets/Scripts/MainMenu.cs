using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string PlayScene; // Name of the scene that will be loaded when "Play" is clicked

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Load the first level
    public void PlayGame()
    {
        SceneManager.LoadScene(PlayScene);
    }

    public void ExitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();

    }

}
