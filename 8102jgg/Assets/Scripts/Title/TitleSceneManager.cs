using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour {

	[SerializeField] private Canvas m_Title;

	public float m_TimeUntilGameStart;
	public float m_FadeOutDuration;

	private float m_Timer = 0.0f;

	private float m_AlphaIncrements;
	private float m_AlphaVal = 0.0f;

	private CanvasGroup m_CG;

	void Start()
	{
		this.m_AlphaIncrements = (1.0f / this.m_FadeOutDuration) * Time.fixedDeltaTime;
		this.m_CG = this.m_Title.GetComponentInChildren<CanvasGroup> ();
		this.m_CG.alpha = this.m_AlphaVal;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.m_Timer < this.m_TimeUntilGameStart) {
			if (this.m_TimeUntilGameStart - this.m_Timer <= this.m_FadeOutDuration) {
				this.m_AlphaVal += this.m_AlphaIncrements;
				this.m_CG.alpha = m_AlphaVal;
			}
			this.m_Timer += Time.fixedDeltaTime;
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ((int)SceneName.GAME);
		}
	}
}
