using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedAnim : MonoBehaviour
{

	Animator anim;
	Renderer ren;
	bool seedOff = true;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		ren = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Input.GetKeyDown (KeyCode.M))
		{
			
			ren.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.M))
		{
			ren.enabled = true;
			anim.SetTrigger ("transform");
		}
	}
}
