using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source : https://answers.unity.com/questions/257569/tag-if-condition-with-ontriggerenter.html

//script that controls which flower the HB has access to, according to the nectar it has consumed
public class FlowerToFlower_beak : MonoBehaviour
{

	//variable that keeps that of which flower the HB must go to
	public static int counter = 0;

	//boolean that keeps track if the HB is close enough of the flower (it can be approach even more to drink nectar)
	//this closeness trigggers the pulsating animation of the stigma
//	private bool closeEnough = false;

//	private bool ApproachIntro = false;

	//this boolean keeps track of the contact between the beak of the HB and the stigma
	//if true, the player can press a button and lets the HB drink the nectar and collect pollen
	private bool accessibleNectar = false;

	//boolean that keeps track if the nectar of a flower was consumed
	private bool NectarCollected = false;

	//array of strings that correspong to the tags of the outter colliders of each flower
//	private string[] flowers = { "Flower1", "Flower2", "Flower3", "Flower4", "Flower5" };


	//public booleans to trigger the text in the introduction
//	public static bool intro_approach = false;
//	public static bool intro_closer = false;
//	public static bool intro_connect = false;
	public static bool intro_drink = false;
	public static bool intro_follow = false;
//	public static bool intro_wrongFlower = false;


	// Use this for initialization
	void Start ()
	{


	}
	
	// Update is called once per frame
	void Update ()
	{


		//if the HB's beak is in contact with the stigma
		if (accessibleNectar) {

			//if we are in the introduction of the game
			if (counter == 0) {

				//set previous boolean to false so the correct message appears
				FlowerToFlower_head.intro_connect = false;

				//set boolean to true so the message can appear in Introduction script
				intro_drink = true;
			}

			//if control is pressed
			if (Input.GetKeyDown (KeyCode.Space) && !NectarCollected) {
				//if we are in the introduction of the game
				if (counter == 0) {

					//set previous boolean to false so the correct message appears
					intro_drink = false;

					//set boolean to true so the message can appear in Introduction script
					intro_follow = true;
				}
				//increment counter : a new flower is the goal
				counter++;

				Debug.Log ("counter " + counter);

				//the nectar has been collected
				NectarCollected = true;

				//ANIMATION : the nectar being consumed && the beak changing color && the flower changing color
			}
		}
	}

	//When the HB collides with the flowers
	void OnTriggerEnter (Collider theCollision)
	{

		//if the HB's beek collides with the stigma
		//***** MAY HAVE TO ADJUST COLLIDER ELEMENT IN UNITY AS IT IS NOT THE ENTIRE BODY OF THE BIRD
		//THAT SHOULD TRIGGER THIS IF STATEMENT BUT ONLY THE BEAK***
		if (theCollision.gameObject.tag == "stigma") {

			

			//the nectar is accessible
			accessibleNectar = true;

			Debug.Log ("Press X to drink nectar");	
			Debug.Log ("accessibleNectar : " + accessibleNectar);
		}
	}

	//When the HB exists the space close to the flowers
	void OnTriggerExit (Collider theCollision)
	{

		//when the HB's beak is no longer in contact with stigma
		if (theCollision.gameObject.tag == "stigma") {

			//set boolean to false
			accessibleNectar = false;	

			Debug.Log ("accessibleNectar : " + accessibleNectar);
		}
	}
}
