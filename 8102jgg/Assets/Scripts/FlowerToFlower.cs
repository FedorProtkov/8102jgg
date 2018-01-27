using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerToFlower : MonoBehaviour
{

	public static int counter = 0;
	private bool closeEnough = false;
	private bool accessibleNectar = false;
	private string[] flowers = { "Flower1", "Flower2", "Flower3", "Flower4", "Flower5" };
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (closeEnough) {

			if (accessibleNectar) {
				Debug.Log ("Press X to drink nectar");

				if (Input.GetKeyDown (KeyCode.Space)) {
					counter++;
					//animation : the nectar being consumed && the beak changing color && the flower changing color


				}
			}

		}
	}

	void OnTriggerEnter (Collider theCollision)
	{
		Debug.Log ("counter " + counter);
		Debug.Log ("flowers[counter] : " + flowers [counter]);


		//while(counter < 5){

		if (theCollision.gameObject.tag == flowers [counter]) {

			//if(theCollision.gameObject.tag = "OutterLayer_RegFlower"){

			//stigma starts to pulse
			closeEnough = true;
			Debug.Log("closeEnough : " + closeEnough);
			Debug.Log ("Possible nectar");
	
		} else {
			Debug.Log ("Not ready for this flower yet");

			//flower closing animation
		}

		//}
	}

	void OnTriggerExit(Collider theCollision){

		if (theCollision.gameObject.tag == flowers [counter]) {
			closeEnough = false;
			Debug.Log("closeEnough : " + closeEnough);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log("ON COLLISION ENTER");
		if (col.gameObject.name == "stigma") {

			accessibleNectar = true;	
			Debug.Log("accessibleNectar : " + accessibleNectar);
			//}

		}
	}

	void OnCollisionExit (Collision col)
	{
		Debug.Log("ON COLLISION EXIT");
		if (col.gameObject.tag == "stigma") {

			accessibleNectar = false;	
			Debug.Log("accessibleNectar : " + accessibleNectar);
			//}

		}
	}

}
