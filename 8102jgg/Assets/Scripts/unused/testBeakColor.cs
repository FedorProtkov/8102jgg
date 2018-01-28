using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBeakColor : MonoBehaviour
{
	public float floor = 0.3f;
	public float ceiling = 1.0f;
	public float oscSpeed = 1.0f;
	public Color beakColor;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Renderer renderer = GetComponent<Renderer> ();
		Material mat = renderer.material;
//		if (Input.GetKeyDown (KeyCode.Z))
//		{
			

		float emission = floor + Mathf.PingPong (Time.time * oscSpeed, ceiling - floor);
//		float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = beakColor; //Replace this with whatever you want for your base color at emission level '1'

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
//		}
	}
}
