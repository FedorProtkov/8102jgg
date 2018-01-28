using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnim : MonoBehaviour
{

	public Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!head.flowerOpen && head.WrongApproached)
		{
			anim.SetTrigger ("close");
		}
		else
			if (head.flowerOpen && !head.WrongApproached)
		{
			anim.SetTrigger ("open");
			anim.SetTrigger ("idle");
		}
//		else
//		if (Input.GetKeyDown (KeyCode.E))
//		{
//			anim.SetTrigger ("idle");
//		}
	}
}

