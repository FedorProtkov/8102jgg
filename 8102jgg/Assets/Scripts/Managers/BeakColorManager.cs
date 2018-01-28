using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakColorManager : MonoBehaviour {

	[SerializeField] GameObject m_Bird;

	private Renderer m_renderer;

	public float floor = 0.3f;
	public float ceiling = 1.0f;
	public float oscSpeed = 1.0f;
	private Color beakColor;
	// Use this for initialization
	void Start ()
	{
		this.beakColor = Color.cyan;
		this.m_renderer = this.m_Bird.GetComponentInChildren<Renderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
//		Renderer renderer = GetComponent<Renderer> ();
		Material mat = this.m_renderer.material;
		float emission = floor + Mathf.PingPong (Time.time * oscSpeed, ceiling - floor);
		Color baseColor = beakColor; //Replace this with whatever you want for your base color at emission level '1'

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}

	public void ChangeBeakColor(Color color)
	{
		this.beakColor = color;
	}
}
