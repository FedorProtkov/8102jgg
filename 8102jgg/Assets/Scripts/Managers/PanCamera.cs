using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{

	[SerializeField] GameObject Selas;
	private CanvasGroup m_CG;

	public float transitionDuration;
	public float m_FadeInDuration;
	private float m_AlphaIncrements;

	private Animator m_Animator;

	public Transform targetObject;

	private Vector3 start;
	private Vector3 target;

	private float time = 0.0f;

	public float adjustX = 0.18f;
	public float adjustY = 1.0f;
	public float adjustZ = 1.0f;

	private bool resetTime = false;

	public static bool selectingUnits = true;
	public static bool atUnits = false;
	public static bool finishedUnits = false;

	private bool doneMoving = false;

	public Camera cam1;
	public Camera cam2;

	float countdownToChange = 5.0f;

	public AudioSource elementSource;
	public AudioSource elementSource1;

	private bool playIntroSound = true;
	private bool playGameSound = false;


	//	public GameObject kodama;

	//Used the following links as a reference
	//https://answers.unity.com/questions/64404/smooth-camera-shift-lerp-smoothshift.html
	//https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html

	void Start ()
	{
		elementSource = gameObject.AddComponent<AudioSource> ();
		elementSource1 = gameObject.AddComponent<AudioSource> ();

		cam1.enabled = true;
		cam2.enabled = false;
		start = transform.position;
		target = targetObject.transform.position;
		target = new Vector3 (target.x + adjustX, target.y + adjustY, target.z - adjustZ);
		this.m_CG = this.GetComponentInChildren<CanvasGroup> ();
		this.m_CG.alpha = 1.0f;
		this.m_AlphaIncrements = (1.0f / this.m_FadeInDuration) * Time.fixedDeltaTime;

		this.m_Animator = this.Selas.GetComponent<Animator> ();
//		loadScene_Button.reload ();
	}

	// Update is called once per frame
	void Update ()
	{

        if (playIntroSound)
		{
			Debug.Log ("playsound");
			elementSource.PlayOneShot ((AudioClip)Resources.Load ("selas_intro_CLEANN"));
			playIntroSound = false;
		}
		if (this.m_CG.alpha > 0.0f) {
			this.m_CG.alpha -= this.m_AlphaIncrements;
			this.m_Animator.SetBool ("FadeInComplete", !(this.m_CG.alpha > 0.0f));
			return;
		}
		if (!cam2.enabled)
		{
			if (selectingUnits)
			{
				StartCoroutine (Transition (start, target));
			}

			if (time >= 1)
			{
				countdownToChange -= Time.deltaTime;

				//when countdown is less than or equal to zero
				if (countdownToChange <= 0.0f)
				{
					playGameSound = true;
					if (playGameSound)
					{
						elementSource1.PlayOneShot ((AudioClip)Resources.Load ("atmosphere"));
//						elementSource1.loop;
						playGameSound = false;
					}

					cam1.enabled = !cam1.enabled;
					cam2.enabled = !cam2.enabled;
				}
			}
		}

		if (!selectingUnits)
		{
			StartCoroutine (Transition (this.transform.position, start));
			finishedUnits = true;
		}

	}

	public IEnumerator Transition (Vector3 startPos, Vector3 targetPos)
	{
		while (time < 1.0f)
		{
			time += Time.deltaTime * (Time.timeScale / transitionDuration);

			transform.position = Vector3.Lerp (startPos, targetPos, time);

			if (transform.position == targetPos)
			{
				atUnits = true;
				time = 1;
			}
			yield return 0;
		}
	}
}