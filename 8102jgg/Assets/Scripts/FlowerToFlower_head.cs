using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source : https://answers.unity.com/questions/257569/tag-if-condition-with-ontriggerenter.html

//script that controls which flower the HB has access to, according to the nectar it has consumed
public class FlowerToFlower_head : MonoBehaviour
{

	//variable that keeps that of which flower the HB must go to
//	public static int counter = 0;

	//boolean that keeps track if the HB is close enough of the flower (it can be approach even more to drink nectar)
	//this closeness trigggers the pulsating animation of the stigma
	private bool closeEnough = false;

	private bool ApproachIntro = false;

	//this boolean keeps track of the contact between the beak of the HB and the stigma
	//if true, the player can press a button and lets the HB drink the nectar and collect pollen
//	private bool accessibleNectar = false;

	//boolean that keeps track if the nectar of a flower was consumed
//	private bool NectarCollected = false;

	//array of strings that correspong to the tags of the outter colliders of each flower
	private string[] flowers = { "Flower1", "Flower2", "Flower3", "Flower4", "Flower5" };


	//public booleans to trigger the text in the introduction
	public static bool intro_approach = false;
	public static bool intro_closer = false;
	public static bool intro_connect = false;
//	public static bool intro_drink = false;
//	public static bool intro_follow = false;
	public static bool intro_wrongFlower = false;


	// Use this for initialization
	void Start ()
	{

		//boolean that allows the first message of the game to display
		intro_approach = true;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ApproachIntro) { 

			//if we are in the introduction of the game
			if (FlowerToFlower.counter == 0) {

				//set previous boolean to false so the correct message appears
				intro_approach = false;

				//set boolean to true so the message can appear in Introduction script
				intro_closer = true;
			}

			//if the HB commes into close range of the flower (boolean is only true when the HB is close to CORRECT flower)
			if (closeEnough) {

				//if we are in the introduction of the game
				if (FlowerToFlower.counter == 0) {

					//set previous boolean to false so the correct message appears
					intro_closer = false;

					//set boolean to true so the message can appear in Introduction script
					intro_connect = true;
				}
			}
		}
	}

	//When the HB collides with the flowers
	void OnTriggerEnter (Collider theCollision)
	{
		Debug.Log ("counter " + FlowerToFlower.counter);
		Debug.Log ("flowers[counter] : " + flowers [FlowerToFlower.counter]);

		//if the HB enters a close range of the FIRST CORRECT flower
		if (theCollision.gameObject.tag == "intro_closer") {
			ApproachIntro = true;
			intro_wrongFlower = false;
		}

		//if the HB enters a close range of the CORRECT flower
		if (theCollision.gameObject.tag == flowers [FlowerToFlower.counter] || theCollision.gameObject.tag == "intro_closer") {
			intro_wrongFlower = false;
			//the HB is close enough to the flower and could proceed to drink nectar
			closeEnough = true;

//			FlowerToFlower_beak.NectarCollected = false;
			//ANIMATION : stigma starts to pulse, indicating to the player to come closer to the stigma

			Debug.Log ("closeEnough : " + closeEnough);
			Debug.Log ("Possible nectar");

			//if the HB enters a close range of the WRONG flower
		}
		 else {

			Debug.Log ("Not ready for this flower yet");

			//if we are in the introduction of the game
			if (FlowerToFlower.counter == 0) {

				//set boolean to true so the message can appear in Introduction script
				intro_wrongFlower = true;
			}

			//ANIMATION : flower closes
		}
	}

	//When the HB exists the space close to the flowers
	void OnTriggerExit (Collider theCollision)
	{

		//when the HB is no longer in close range of the flower
		if (theCollision.gameObject.tag == flowers [FlowerToFlower.counter] || theCollision.gameObject.tag == "intro_closer") {

			intro_wrongFlower = false;
			//set boolean to false
			closeEnough = false;
			intro_connect = false;
			FlowerToFlower_beak.intro_drink = false;
			FlowerToFlower_beak.intro_follow = false;
			Debug.Log ("closeEnough : " + closeEnough);

			//when the HB is no longer in close range of the WRONG flower
		} else {

			//ANIMATION : flower opens again
		}
	}
}
