//#define TESTING_INPUTS
//#define TESTING_VELOCITY_RELATED
//Uncomment this macro if you're using a PS4 controller
#define PS4_CONTROLLER
//Uncomment this macro if you're using a PS3 controller
//#define PS3_CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	/**A reference to the gameobject containing all the player components (namely, the camera and the player gameobject).
	*We apply translations to this gameobject and therefore move the player and camera simultaneously.*/
	[SerializeField] GameObject m_PlayerContainer;
	/**A reference to the actual player gameobject*/
	[SerializeField] GameObject m_PlayerGameObject;
	/**A reference to the bird's head gameobject*/
	[SerializeField] GameObject m_BirdHead;

	[SerializeField] GameObject m_BirdBody;

	/**A variable we need to introduce to account for my shitty controller. As it is, we have small issues with the sensitivity of the joysticks.
	*This value will therefore be what the input of a given joystick needs to be greater than in order to be considered valid input.
	*i.e. if [m_ControllerJoystickErrorMargin] = 0.1, then input must be (input < -(0.1) || 0.1 > input).*/
	public float m_ControllerJoystickErrorMargin;

	/**The maximal speed at which the player can move (this serves as an upper-bound).
	*This velocity is independent of the y-bound velocity, and serves only to limit x- and z-ward motion.*/
	public float m_TerminalVelocity;
	/**The current speed at which the player is moving.*/
	private float m_CurrentVelocity = 0.0f;
	/**The rate at which the bird reaches the terminal velocity.*/
	public float m_Acceleration;
	/**The current direction in which we're moving, if any.*/
	private Vector3 m_CurrentDirection = Vector3.zero;
	/**The maximal speed at which the player can rotate the camera about the y-axis*/
	public float m_MaximalRotationSpeed;

	/**The name of the input axis responsible for the horizontal component of the left joystick*/
	public static readonly string INPUT_CONTROLLER_LEFTJOYSTICK_X = "Left Joystick X";
	/**The name of the input axis responsible for the vertical component of the left joystick*/
	public static readonly string INPUT_CONTROLLER_LEFTJOYSTICK_Y = "Left Joystick Y";
	/**The name of the input axis responsible for the horizontal component of the right joystick*/
	public static readonly string INPUT_CONTROLLER_RIGHTJOYSTICK_X = "Right Joystick X";
	/**The name of the input button corresponding to the controller X button*/
	public static readonly string INPUT_CONTROLLER_BUTTON_X = "Controller X";
	/**The name of the input button corresponding to the controller square button*/
	public static readonly string INPUT_CONTROLLER_BUTTON_SQUARE = "Controller Square";
	/**The name of the input button corresponding to the PS4 controller X button*/
	public static readonly string INPUT_PS4_CONTROLLER_BUTTON_X = "PS4 Controller X";
	/**The name of the input button corresponding to the PS4 controller square button*/
	public static readonly string INPUT_PS4_CONTROLLER_BUTTON_SQUARE = "PS4 Controller Square";

	/**The layer containing all objects belonging to the scene that specifically cannot be passed through.*/
	public static readonly string LAYER_SCENERY = "Scenery";

	// Update is called once per frame
	void Update ()
	{
		this.ManagePlayerInputForMovement ();
		this.ManagePlayerInputForCameraRotation ();

	}

	/**A function to manage movement with respect to user input.*/
	private void ManagePlayerInputForMovement ()
	{
		#if TESTING_INPUTS
		string message = "";
		#endif

		float left_joystick_horizontal_input = Input.GetAxis (INPUT_CONTROLLER_LEFTJOYSTICK_X);
		float left_joystick_vertical_input = Input.GetAxis (INPUT_CONTROLLER_LEFTJOYSTICK_Y);
		#if PS3_CONTROLLER
		float controller_rise_input = Input.GetButton (INPUT_CONTROLLER_BUTTON_X) ? 1.0f : 0.0f;
		float controller_lower_input = Input.GetButton (INPUT_CONTROLLER_BUTTON_SQUARE) ? -1.0f : 0.0f;
		#elif PS4_CONTROLLER
		float controller_rise_input = Input.GetButton (INPUT_PS4_CONTROLLER_BUTTON_X) ? 1.0f : 0.0f;
		float controller_lower_input = Input.GetButton (INPUT_PS4_CONTROLLER_BUTTON_SQUARE) ? -1.0f : 0.0f;
		#endif

		float y_ward_input = controller_rise_input + controller_lower_input;

		#if TESTING_INPUTS
		if (controller_rise_input != 0.0f){
			message += "X button detected\n" ;
		}
		if (controller_lower_input != 0.0f) {
			message += "Square button detected\n";
		}
		#endif

		Vector3 displacement_to_apply = Vector3.zero;

		//if any left joystick or button (X or Square) input is detected...
		if (this.WasLeftJoystickInputDetectedAndValid (left_joystick_horizontal_input, left_joystick_vertical_input)
		    || y_ward_input != 0.0f)
		{
			//if we're currently moving or at rest, and adding to the velocity won't surpass our speed limit...
			if (this.m_CurrentVelocity >= 0.0f
			    && this.m_CurrentVelocity + this.m_Acceleration * Time.fixedDeltaTime <= this.m_TerminalVelocity)
			{
				//...then add to our current speed
				this.m_CurrentVelocity += this.m_Acceleration * Time.fixedDeltaTime;
			}

			//X-ward displacement
			float horizontal_displacement = 0.0f;
			//Z-ward displacement
			float vertical_displacement = 0.0f;

			//if there was specifically a horizontal input...
			if (left_joystick_horizontal_input != 0.0f)
			{
				#if TESTING_INPUTS
				message += "Left joystick horizontal input detected\n";
				#endif
				horizontal_displacement = left_joystick_horizontal_input * this.m_CurrentVelocity;
			}
			//if there was specifically a vertical input...
			if (left_joystick_vertical_input != 0.0f)
			{
				#if TESTING_INPUTS
				message += "Left joystick vertical input detected\n";
				#endif
				vertical_displacement = left_joystick_vertical_input * this.m_CurrentVelocity;
			}

			//Make up new displacement vector from the respective inputs
			displacement_to_apply = new Vector3 (horizontal_displacement, 0.0f, vertical_displacement);

			float y_ward_displacement = 0.0f;
			//if there was either an upward or downward input...
			if (y_ward_input != 0.0f)
			{
				//if y_ward_input is positive, we go up
				//else we go down
				y_ward_displacement = y_ward_input * this.m_CurrentVelocity;
				displacement_to_apply.y = y_ward_displacement;
			}//end if
			//else if y_ward is either no input, or a combination of upward and downward inputs, do nothing.

			//if the displacement to apply results in a movement that is faster than our maximal velocity, then clamp the velocity
			if (displacement_to_apply.magnitude > this.m_TerminalVelocity)
			{
				displacement_to_apply = Vector3.ClampMagnitude (displacement_to_apply, this.m_TerminalVelocity);
			}
			//Apply Time.fixedDeltaTime to make movement frame-rate independent
			displacement_to_apply *= Time.fixedDeltaTime;

			displacement_to_apply = this.ConvertDisplacementToBeCameraSubjective (displacement_to_apply);

			//Apply transformation
//			this.CheckForCollisionAndApplyDisplacementIfPossible(displacement_to_apply);
			this.m_PlayerContainer.transform.position += displacement_to_apply;

			this.m_CurrentDirection = Vector3.Normalize (displacement_to_apply);
		}//end if
		//else if no movement input is detected...
		else
		{
			if (this.m_CurrentVelocity > 0.0f)
			{
				//...then decrease the current velocity
				this.m_CurrentVelocity -= this.m_Acceleration * 5.0f * Time.fixedDeltaTime;
				if (this.m_CurrentVelocity < 0.0f)
				{
					this.m_CurrentVelocity = 0.0f;
				}
				//...and apply the displacement on the bird
				this.m_PlayerContainer.transform.position += Vector3.ClampMagnitude (this.m_CurrentDirection, this.m_CurrentVelocity * Time.fixedDeltaTime);
			}

		}//end else



		#if TESTING_INPUTS
		if (message != "")
		{
			Debug.Log(message);
		}
		#endif

		#if TESTING_VELOCITY_RELATED
		Debug.Log("Current velocity: " + this.m_CurrentVelocity + "\n"
			+ "Acceleration: " + this.m_Acceleration);
		#endif

	}
//end f'n void ManagePlayerInputForMovement()

	/**A function to take care of rotating the camera with respect to player input*/
	private void ManagePlayerInputForCameraRotation ()
	{
		#if TESTING_INPUTS
		string message = "";
		#endif
		float rotation = Input.GetAxis (INPUT_CONTROLLER_RIGHTJOYSTICK_X);

		if (rotation != 0.0f && this.DoesInputSurpassJoystickErrorMargin (rotation))
		{
			#if TESTING_INPUTS
			message += "Right joystick horizontal input detected.\n";
			#endif

			this.m_PlayerContainer.transform.Rotate (0.0f, rotation * this.m_MaximalRotationSpeed * Time.fixedDeltaTime, 0.0f);
			this.m_PlayerGameObject.transform.Rotate (0.0f, -(rotation * this.m_MaximalRotationSpeed * Time.fixedDeltaTime), 0.0f);
		}

		#if TESTING_INPUTS
		if (message != "")
		{
			Debug.Log(message);
		}
		#endif
	}

	/**Returns true if the given value valid with respect to the joystick error margin.*/
	private bool DoesInputSurpassJoystickErrorMargin (float input)
	{
		return -(this.m_ControllerJoystickErrorMargin) > input || input > this.m_ControllerJoystickErrorMargin;
	}

	private bool WasLeftJoystickInputDetectedAndValid (float left_joystick_horizontal_input, float left_joystick_vertical_input)
	{
		return((left_joystick_horizontal_input != 0.0f || left_joystick_vertical_input != 0.0f)
		&& (this.DoesInputSurpassJoystickErrorMargin (left_joystick_horizontal_input)
		|| this.DoesInputSurpassJoystickErrorMargin (left_joystick_vertical_input)));
	}

	/**A function to return a vector that contains the input collected from the user, converted to a motion vector relative to the camera*/
	private Vector3 ConvertDisplacementToBeCameraSubjective (Vector3 displacement_to_apply)
	{
		Camera camera = this.m_PlayerContainer.transform.GetComponentInChildren<Camera> ();
		Vector3 vector_to_return = Vector3.zero;
		Vector3 x_plus = Vector3.ClampMagnitude (camera.transform.right, displacement_to_apply.x);
		vector_to_return = new Vector3 (0.0f, displacement_to_apply.y, 0.0f);
		vector_to_return = vector_to_return + x_plus;
		Vector3 z_plus = camera.transform.forward;
		z_plus.y = 0.0f;
		z_plus = Vector3.ClampMagnitude (z_plus, displacement_to_apply.z);
		vector_to_return = vector_to_return + z_plus;
		return vector_to_return;
	}

	/**A function to check to see whether applying a given displacement to the bird leads to a collision.
	*If so, the displacement is not applied.*/
	private void CheckForCollisionAndApplyDisplacementIfPossible(Vector3 displacement_to_apply)
	{
		int scenery_layermask = UnityEngine.LayerMask.NameToLayer (LAYER_SCENERY);

//		Debug.Log ("Player container position: " + this.m_PlayerContainer.transform.position.x + ", "
//			+ this.m_PlayerContainer.transform.position.y + ", "
//			+ this.m_PlayerContainer.transform.position.z);

		foreach (RaycastHit hit in Physics.RaycastAll(this.m_BirdHead.gameObject.transform.position, displacement_to_apply, displacement_to_apply.magnitude * 25.0f)) {
			if (hit.collider.gameObject.layer == UnityEngine.LayerMask.NameToLayer ("Scenery")) {
				Debug.Log ("Hit scenery element " + hit.collider.gameObject.name);
				this.m_CurrentVelocity = 0.0f;
				return;
			}
		}
		this.m_PlayerContainer.transform.position += displacement_to_apply;
	}
}
