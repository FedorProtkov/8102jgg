﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head : MonoBehaviour
{

	//array of strings that correspong to the tags of the outter colliders of each flower
	private string[] flowers = { "Flower1", "Flower2", "Flower3", "Flower4", "Flower5" };

	//public booleans to trigger the text in the introduction
	public static bool intro_approach = false;
	public static bool intro_closer = false;
	public static bool intro_connect = false;
	//	public static bool intro_drink = false;
	//	public static bool intro_follow = false;
	public static bool intro_wrongFlower = false;

	//boolean that keeps track if the HB is close enough of the flower (it can be approach even more to drink nectar)
	//this closeness trigggers the pulsating animation of the stigma
	private bool closeEnough = false;


	private bool ApproachIntro = false;


	// Use this for initialization
	void Start ()
	{
		intro_approach = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//if in intro
		if (ApproachIntro) { 

			//if we are in the introduction of the game
			if (beak.counter == 0) {

				//set previous boolean to false so the correct message appears
				intro_approach = false;

				//set boolean to true so the message can appear in Introduction script
				intro_closer = true;
			}
		}
		//if the HB commes into close range of the flower (boolean is only true when the HB is close to CORRECT flower)
		if (closeEnough) {

			//if we are in the introduction of the game
			if (beak.counter == 0) {

				//set previous boolean to false so the correct message appears
				intro_closer = false;

				//set boolean to true so the message can appear in Introduction script
				intro_connect = true;
			}


		}
	}

	//When the HB collides with the flowers
	void OnTriggerEnter (Collider theCollision){

		//if the HB enters a close range of the FIRST CORRECT flower
		if (theCollision.gameObject.tag == "intro_closer") {
			ApproachIntro = true;
			intro_wrongFlower = false;
		}


		//if the HB enters a close range of the CORRECT flower
		if (theCollision.gameObject.tag == flowers [beak.counter]) {
			intro_wrongFlower = false;
			//the HB is close enough to the flower and could proceed to drink nectar
			closeEnough = true;

			//ANIMATION : stigma starts to pulse, indicating to the player to come closer to the stigma


		}

	}


	//When the HB exists the space close to the flowers
	void OnTriggerExit (Collider theCollision)
	{

		//when the HB is no longer in close range of the flower
		if (theCollision.gameObject.tag == flowers [beak.counter]) {

			intro_wrongFlower = false;
			closeEnough = false;
			intro_connect = false;

			//when the HB is no longer in close range of the WRONG flower
		} else {

			//ANIMATION : flower opens again
		}


		if (theCollision.gameObject.tag == "intro_closer") {
		ApproachIntro = false;
		}
	}
}
