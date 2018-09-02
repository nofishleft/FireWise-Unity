using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public static int healthMax = 100;
    public float healthDecay = 1f; // How much health the player will lose per second
    public float moveFactor = 0.5f; // How much decay will be reduced by when the player is travelling at their max speed

    public static float health;

    public static AudioSource bounceaudio;
    public static AudioClip bounceclip;
    public static bool hasnotplayed = true;
    private AudioSource[] allAudioSources;

    // Use this for initialization
    void Start () {
        health = healthMax;
        bounceaudio = gameObject.AddComponent<AudioSource>();
        bounceclip = (AudioClip)Resources.Load("lake");
    }

    // Update is called once per frame
    void Update () {
        float decay;
        float speedPercent;

        // Determine how much of the player's potential speed they are travelling
        speedPercent = (PlayerMovement.speed - PlayerMovement.speedMin) / (PlayerMovement.speedMax - PlayerMovement.speedMin);

        // Calculate the net decay after taking into account the player's movement speed
        decay = healthDecay * (1 - moveFactor * speedPercent);

        health -= decay * Time.deltaTime;
        if(GameVictory.victory && hasnotplayed){
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            bounceaudio.PlayOneShot(bounceclip);
            hasnotplayed = false;
        }
	}
}
