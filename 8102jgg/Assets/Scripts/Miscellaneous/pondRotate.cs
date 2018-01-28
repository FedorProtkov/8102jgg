using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pondRotate : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Transform.rotate(
		transform.Rotate (Vector3.down * (Time.deltaTime * 3));
	}
}
