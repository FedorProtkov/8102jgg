using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour {

	public float m_TimeUntilGameStart;

	private float m_Timer = 0.0f;

	
	// Update is called once per frame
	void Update () {
		if (this.m_Timer < this.m_TimeUntilGameStart) {
			this.m_Timer += Time.fixedDeltaTime;
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ((int)SceneName.GAME);
		}
	}
}
