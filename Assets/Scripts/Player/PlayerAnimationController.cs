using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("fire", true);
        
	}
	
	// Update is called once per frame
	void Update () {
        //PlayerMovement move = GetComponent<PlayerMovement>();
        anim.SetInteger("xdir", PlayerMovement.xdir);
        anim.SetInteger("ydir", PlayerMovement.ydir);
        anim.SetFloat("health", PlayerHealth.health);
        float adjustedSpeed = 1+(PlayerMovement.speed - PlayerMovement.speedMin)/(PlayerMovement.speedMax-PlayerMovement.speedMin);
        anim.SetFloat("speed", adjustedSpeed);
	}
}
