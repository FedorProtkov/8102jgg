﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selasAnim : MonoBehaviour
{

	Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			anim.SetTrigger ("transform");
		}
		if (Input.GetKeyDown (KeyCode.N))
		{
			anim.SetTrigger ("seed");
		}
	}
}
