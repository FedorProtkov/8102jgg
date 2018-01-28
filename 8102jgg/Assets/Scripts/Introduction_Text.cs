using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//source: https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial/displaying-score-and-text

//Script that controls how the text appears in the introduction of the game
public class Introduction_Text : MonoBehaviour
{

	public Text intro;
	private bool introduction = true;
	private string[] messages = {"Approach the flower", "Closer", "Connect the beak", "Press x to drink nectar",
		"Follow the color of your beak", "Approach the correct flower"
	};

	// Use this for initialization
	void Start ()
	{
		intro.text = "";	
	}
	
	// Update is called once per frame
	void Update ()
	{

		//if the player is wondering for what to do first
		if (beak.counter <= 1) {

			//we are in introduction mode
			introduction = true;

			//if the player has collected nectar from other flowers
		} else {

			//we are no longer in introduction mode
			introduction = false;
		}

		//if the player is in the introduction of the game
		if (introduction) {

			//if the boolean is true, meaning the player is in the stage of approaching the flower for the first time 
			if (head.intro_approach) {

				//display the approach message
				intro.text = messages [0];
			}

			//if the boolean is true, meaning the player is in the stage of getting a little closer to the flower
			if (head.intro_closer) {

				//display the closer message
				intro.text = messages [1];
			}

			//if the boolean is true, meaning the player is in the stage of connecting the beak to the stigma
			if (head.intro_connect) {

				//display the connect message
				intro.text = messages [2];
			}

			//if the boolean is true, meaning the player is in the stage of drinking the nectar
			if (beak.intro_drink) {

				//display the drink message
				intro.text = messages [3];
			}

			//if the boolean is true, meaning the player is in the stage of follow the color of its beak
			if (beak.intro_follow) {

				//display the follow message
				intro.text = messages [4];
			}

			//if the player approaches the wrong flower
			if (head.intro_wrongFlower) {

				//display the message
				intro.text = messages [5];
			}
		}else{
			intro.text = "";	
		}

	}
}
