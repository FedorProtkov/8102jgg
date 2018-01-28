using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingFireflyManager : MonoBehaviour {
	/**A reference to the firefly that guides the player to the next flower to be pollenated*/
	[SerializeField] GameObject m_GuidingFirefly;
	[SerializeField] GameObject m_Player;

	[SerializeField] Camera m_MainCamera;

	[SerializeField] List<GameObject> m_Flowers;

	private bool m_PlayerBeenCircled = false;

	public float m_FireflyMovementSpeed;

	private BeakColorManager m_BCM;

	private ParticleSystem m_PS;

	void Start()
	{
		this.m_BCM = this.GetComponent<BeakColorManager> ();
		this.m_PS = this.m_GuidingFirefly.GetComponent<ParticleSystem> ();
		Color color = this.m_PS.main.startColor.color;
		color = this.m_BCM.GetCurrentBeakColor();
//		this.m_PS.main.startColor.color = color;

	}

	// Update is called once per frame
	void Update () {
		if (this.m_MainCamera.enabled) {
			//go to next plant

			//hover above plant

		}
	}

	private void ManageMovement()
	{

	}

	private void CircleFlower()
	{

	}
}
