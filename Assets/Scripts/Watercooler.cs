using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercooler : MonoBehaviour
{

    bool consumed = false;

    public Renderer rend;
    public float healthRestoreFlat = 0f;
    public float healthRestorePercent = 0.7f; // How much of the user's max health to restore

    public int playerLayerID = 10; // ID of the layer the player is on

    public static AudioSource drinkAudio;
    public static AudioClip drinkClip;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        drinkAudio = gameObject.AddComponent<AudioSource>();
        drinkClip = (AudioClip)Resources.Load("waterget2_mixdown");
    }



    // If the player walks into this, heal them (but not above their maximum health)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if ((collision.gameObject.layer == playerLayerID) && rend.enabled)
        {
            drinkAudio.PlayOneShot(drinkClip);
            PlayerHealth.health += healthRestoreFlat + (PlayerHealth.healthMax * healthRestorePercent);
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, PlayerHealth.healthMax);
            rend.enabled = false;
        }

    }
}
