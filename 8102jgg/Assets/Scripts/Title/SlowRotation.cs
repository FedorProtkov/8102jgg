using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotation : MonoBehaviour {

	public float m_RotationSpeed;
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0.0f, this.m_RotationSpeed, 0.0f));
	}
}
