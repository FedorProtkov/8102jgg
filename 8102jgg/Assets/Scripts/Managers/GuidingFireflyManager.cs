using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingFireflyManager : MonoBehaviour {
	/**A reference to the firefly that guides the player to the next flower to be pollenated*/
	[SerializeField] GameObject m_GuidingFirefly;
	[SerializeField] GameObject m_Player;

	[SerializeField] Camera m_MainCamera;

	[SerializeField] List<GameObject> m_Flowers;

	private int m_FlowerInt = 0;

	private bool m_PlayerBeenCircled = false;

	public float m_FireflyMovementSpeed;

	private BeakColorManager m_BCM;

	private ParticleSystem m_PS;

	void Start()
	{
		this.m_BCM = this.GetComponent<BeakColorManager> ();
		this.m_PS = this.m_GuidingFirefly.GetComponent<ParticleSystem> ();
		ParticleSystem.MainModule settings = this.m_PS.main;
		settings.startColor = new ParticleSystem.MinMaxGradient(this.m_BCM.GetCurrentBeakColor ());
	}

	// Update is called once per frame
	void Update () {
		if (this.m_MainCamera.enabled) {
			//go to next plant
			this.ManageMovement();
		}

		//Pollenate
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.m_BCM.ChangeBeakColorToNextInSequence ();
			this.ChaseNextFlower ();
		}
	}

	private void ManageMovement()
	{
		if (this.m_GuidingFirefly.transform.position != this.m_Flowers [this.m_FlowerInt].transform.position + (Vector3.up * 5.0f)) {
			this.m_GuidingFirefly.transform.position += Vector3.ClampMagnitude(this.m_Flowers[m_FlowerInt].transform.position + (Vector3.up * 10.0f) - this.m_GuidingFirefly.transform.position, this.m_FireflyMovementSpeed * Time.fixedDeltaTime);
		}
	}

	private void ChaseNextFlower()
	{
		this.m_FlowerInt++;
		ParticleSystem.MainModule settings = this.m_PS.main;
		settings.startColor = new ParticleSystem.MinMaxGradient(this.m_BCM.GetCurrentBeakColor ());
	}

	public void Pollenate()
	{
		this.m_BCM.ChangeBeakColorToNextInSequence ();
		this.ChaseNextFlower ();
	}
}