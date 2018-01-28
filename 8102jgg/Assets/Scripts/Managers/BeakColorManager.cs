using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakColorManager : MonoBehaviour {

	[SerializeField] GameObject m_Bird;

	private Renderer m_renderer;

	public float floor;
	public float ceiling;
	public float oscSpeed;
	private Color beakColor;
	/**An array of all our colors, to facilitate switching to the next color in the sequence.*/
	private Color[] all_colors = { Color.blue, Color.red, Color.blue, Color.white, 
								Color.magenta, Color.yellow, Color.cyan, Color.red};
	/**An int representative of the index of the current color*/
	private int m_ColorIndex = 0;


	// Use this for initialization
	void Start ()
	{
		this.beakColor = all_colors[this.m_ColorIndex];
		this.m_renderer = this.m_Bird.GetComponentInChildren<Renderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
		Material mat = this.m_renderer.material;
		float emission = floor + Mathf.PingPong (Time.time * oscSpeed, ceiling - floor);
		Color baseColor = beakColor; //Replace this with whatever you want for your base color at emission level '1'

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}

	/**A function to change the beak color to a given color.*/
	public void ChangeBeakColor(Color color)
	{
		this.beakColor = color;
	}

	/**A color to change the beak color to the next color in the sequence.*/
	public void ChangeBeakColorToNextInSequence()
	{
		this.m_ColorIndex++;
		this.beakColor = this.all_colors [this.m_ColorIndex];
	}

	/**A function to return the current color of the beak; this is meant to help us with the guiding particle effects.*/
	public Color GetCurrentBeakColor()
	{
		return this.beakColor;
	}
}
