//#define TESTING_INPUTS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	/**A reference to the gameobject containing all the player components (namely, the camera and the player gameobject).
	*We apply translations to this gameobject and therefore move the player and camera simultaneously.*/
	[SerializeField] GameObject m_PlayerContainer;

	/**The maximal speed at which the player can move (this serves as an upper-bound).*/
	public float m_TerminalVelocity;
	/**The maximal speed at which the player can rotate the camera about the y-axis*/
	public float m_MaximalRotationSpeed;

	/**The name of the input axis responsible for the horizontal component of the left joystick*/
	public static readonly string INPUT_CONTROLLER_LEFTJOYSTICK_X = "Left Joystick X";
	/**The name of the input axis responsible for the vertical component of the left joystick*/
	public static readonly string INPUT_CONTROLLER_LEFTJOYSTICK_Y = "Left Joystick Y";




	// Use this for initialization
	void Start () {
//		Debug.Log ("Hello!");
	}
	
	// Update is called once per frame
	void Update () {
		this.ManagePlayerInputForMovement ();
	}

	/**A function to manage movement with respect to user input.*/
	private void ManagePlayerInputForMovement()
	{
		#if TESTING_INPUTS
		string message = "";
		#endif

		float left_joystick_horizontal_input = Input.GetAxis (INPUT_CONTROLLER_LEFTJOYSTICK_X);
		float left_joystick_vertical_input = Input.GetAxis (INPUT_CONTROLLER_LEFTJOYSTICK_Y);
		//if any left joystick input is detected...
		if (left_joystick_horizontal_input != 0.0f || left_joystick_vertical_input != 0.0f) {
			Vector3 displacement_to_apply = Vector3.zero;
			float horizontal_displacement = 0.0f;
			float vertical_displacement = 0.0f;

			//if there was specifically a horizontal input...
			if (left_joystick_horizontal_input != 0.0f) {
				#if TESTING_INPUTS
				message += "Left joystick horizontal input detected\n";
				#endif
				horizontal_displacement = left_joystick_horizontal_input * this.m_TerminalVelocity;
			}
			//if there was specifically a vertical input...
			if (left_joystick_vertical_input != 0.0f) {
				#if TESTING_INPUTS
				message += "Left joystick vertical input detected\n";
				#endif
				vertical_displacement = left_joystick_vertical_input * this.m_TerminalVelocity;
			}

			#if TESTING_INPUTS
			Debug.Log(message);
			#endif

			//Make up new displacement vector from the respective inputs
			displacement_to_apply = new Vector3 (horizontal_displacement, 0.0f, vertical_displacement);
			//if the displacement to apply results in a movement that is faster than our maximal velocity, then clamp the velocity
			if (displacement_to_apply.magnitude > this.m_TerminalVelocity) {
				displacement_to_apply = Vector3.ClampMagnitude (displacement_to_apply, this.m_TerminalVelocity);
			}
			//Apply Time.fixedDeltaTime to make movement frame-rate independent
			displacement_to_apply *= Time.fixedDeltaTime;
			//Apply transformation
			this.m_PlayerContainer.transform.position += displacement_to_apply;
		}//end if
	}//end f'n void ManagePlayerInputForMovement()
}
