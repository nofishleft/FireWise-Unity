using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beartrap : MonoBehaviour {

    public float duration = 3f;
    public float slowFlat = 0f; // Flat amount to subtract from the player's speed
    public float slowPercent = 0.5f; 
    public float damage = 10f;

    public int playerLayerID = 10; // ID of the layer the player is on

    Animator anim;

    public static AudioSource bounceaudio;
    public static AudioClip bounceclip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("open", true);
        bounceaudio = gameObject.AddComponent<AudioSource>();
        bounceclip = (AudioClip)Resources.Load("trap");
    }

    // If the player walks into this trap, inflict damage and increment their debuff modifiers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if ((collision.gameObject.layer == playerLayerID) && anim.GetBool("open"))
        {
            bounceaudio.PlayOneShot(bounceclip);
            PlayerHealth.health -= damage;
            PlayerMovement.slowDuration += duration;
            PlayerMovement.slowPercent = slowPercent;
            anim.SetBool("open", false);

        }

    }
}
