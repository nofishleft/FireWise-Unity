using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public static int[] beatLevel; // 1 = beat level, -1 = died, 0 = initial 
    public static int[] scores;

    static int scenes;
    public static int currentScene = 1; // First level starts at build index 1. The main menu should be the first scene in the build settings.

    static bool playing = false;

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

    // Tells the game manager that the currently loaded level was beaten
    public void Victory()
    {
        GameManager.beatLevel[currentScene] = 1;
    }

    public void Defeat()
    {
        GameManager.beatLevel[currentScene] = -1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); // Restart the current scene until the player wins
    }

    // Use this for initialization
    void Start () {
        scenes = SceneManager.sceneCountInBuildSettings - 1; // The first scene 
        Debug.Log("Scenes = " + scenes);
        beatLevel = new int[scenes];
    }

    // Update is called once per frame
    void Update() {

        // Load the first level and wait until the player reaches some outcome.
        // If they get to the end of that level, load the next level, if not, display 
        // their score before taking them back to the main menu
        if (playing) {
            NextLevel();
        }
    }

    public void PlayGame()
    {
        playing = true;
        Debug.Log("Playing = " + playing);
    }

    public void ExitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

    void NextLevel() { 
        
        //Debug.Log("Current level outcome: " + beatLevel[currentScene]);
        Debug.Log("Active scene buildID: " + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Current scene ID: " + currentScene);

        // If a scene is currently running, do nothing until it reaches some outcome
        if ((SceneManager.GetActiveScene().buildIndex) == currentScene)
            if (beatLevel[currentScene] == 1)
            {
                Debug.Log("Beat Level");
                currentScene++;
            } else if (beatLevel[currentScene] == -1)
            {
                // If the player is defeated, just restart the current scene.
                Debug.Log("Playing = " + playing + " - due to defeat");
                return;
            } else
            {
                // If we get here, the player hasn't won or lost yet, so do nothing.
                return;
            }

        // While there are still scenes to be run, play them one after the other.
        // If they player gets through all the available scenes they'll be returned to the main menu.
        if (currentScene < scenes)
        {
            SceneManager.LoadSceneAsync(currentScene);
            Debug.Log("Active scene buildID: " + SceneManager.GetActiveScene().buildIndex);
        } else
        {
            currentScene = 0;
            playing = false;
            Debug.Log("Playing = " + playing + " - due to end of scenes");
            SceneManager.LoadSceneAsync(0);
        }    
    }

    // Reset progress so that the player can start again
    private void Reset()
    {
        beatLevel = new int[scenes]; // 1 = beat level, -1 = died, 0 = initial 
        scores = new int[scenes];

        currentScene = 1; // First level starts at build index 1. The main menu should be the first scene in the build settings.

        playing = false;
    }
}
