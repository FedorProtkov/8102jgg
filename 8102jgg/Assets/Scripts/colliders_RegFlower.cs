using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source : https://answers.unity.com/questions/257569/tag-if-condition-with-ontriggerenter.html

public class colliders_RegFlower : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("hit outter collider");	
	}

	void OnTriggerEnter(Collider theCollision){

		if (theCollision.gameObject.tag == "OutterLayer_RegFlower"){
		Debug.Log("hit outter collider");
		}

		if(theCollision.gameObject.tag == "stigma"){
			Debug.Log("hit the stigma");
		}
	}
}
