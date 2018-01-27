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
		"Follow the color of your beak"
	};
	// Use this for initialization
	void Start ()
	{
		intro.text = "";	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the player is in the introduction of the game
		if (introduction) {

			//if the player is wondering for what to do first
			if (FlowerToFlower.counter == 0) {
				intro.text = messages [0];
			}
		}
		
	}


}
