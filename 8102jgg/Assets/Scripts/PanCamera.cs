using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
	public float transitionDuration = 2.5f;

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

	public GameObject kodama;

	//Used the following links as a reference
	//https://answers.unity.com/questions/64404/smooth-camera-shift-lerp-smoothshift.html
	//https://answers.unity.com/questions/49542/smooth-camera-movement-between-two-targets.html

	void Start ()
	{
		start = transform.position;
		target = targetObject.transform.position;
		target = new Vector3 (target.x + adjustX, target.y + adjustY, target.z - adjustZ);
//		loadScene_Button.reload ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (selectingUnits)
		{
			StartCoroutine (Transition (start, target));
		}

//		if (!resetTime)
//		{
//			if (ConcentrationConfig.correctScroll)
//			{
//				selectingUnits = false;
//				time = 0;
//				resetTime = true;
//			}
//		}

		if (!selectingUnits)
		{
			StartCoroutine (Transition (this.transform.position, start));
			finishedUnits = true;
		}

		if (finishedUnits)
		{
			kodama.gameObject.GetComponent<CanvasGroup> ().alpha = 0;
			Destroy (kodama.gameObject);
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