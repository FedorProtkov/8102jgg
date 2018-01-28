using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBeakColor : MonoBehaviour
{
	public float floor = 0.3f;
	public float ceiling = 1.0f;
	public float oscSpeed = 1.0f;
	private Color beakColor;
	// Use this for initialization
	void Start ()
	{
		this.beakColor = Color.cyan;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Renderer renderer = GetComponent<Renderer> ();
		Material mat = renderer.material;
		float emission = floor + Mathf.PingPong (Time.time * oscSpeed, ceiling - floor);
//		float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = beakColor; //Replace this with whatever you want for your base color at emission level '1'

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}

	public void ChangeBeakColor(Color color)
	{
		this.beakColor = color;
	}
}
