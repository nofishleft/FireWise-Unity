using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public static int[] beatLevel; // 1 = beat level, -1 = died, 0 = initial 
    public static int[] scores;

    static int scenes;
    public static int currentScene = 0; // First level starts at build index 0. The main menu should be the last scene in the build settings.

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
    }

    // Use this for initialization
    void Start () {
        scenes = SceneManager.sceneCountInBuildSettings - 1; // Subtract 1 as we don't count the main menu as a playable level
        Debug.Log(scenes);
        beatLevel = new int[scenes];
    }

    // Update is called once per frame
    void Update() {

        Debug.Log("Playing = " + playing);
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
    }

    public void ExitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();

    }

    void NextLevel() {
        
        Debug.Log("Active scene buildID: " + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Current level outcome: " + beatLevel[currentScene]);

        // If a scene is currently running, do nothing until it reaches some outcome
        if (SceneManager.GetActiveScene().buildIndex == currentScene)
            if (beatLevel[currentScene] == 1)
            {
                Debug.Log("Beat Level");
                currentScene++;
            } else if (beatLevel[currentScene] == -1)
            {
                currentScene = 0;
                playing = false;
                return;
            } else
            {
                return;
            }

        // While there are still scenes to be run, play them one after the other
        if (currentScene < scenes)
        {
            SceneManager.LoadSceneAsync(currentScene);
        } else
        {
            currentScene = 0;
            playing = false;
        }    

    }
}
