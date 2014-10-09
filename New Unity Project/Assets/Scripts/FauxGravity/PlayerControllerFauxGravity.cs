using UnityEngine;
using System.Collections;

public class PlayerControllerFauxGravity : MonoBehaviour {

	public float moveSpeed;
	public static Vector3 moveDirection;
	private Transform myTransform;
	public float deltaGround;
	public static bool isGrounded;
	private bool doubleJump = false;
	private bool jump = false;
	private float jumpPower = 200;
	public static bool jumping = false;
	public static bool walking = false;
	private Vector3 groundLevel;
	public float jumpHeight;
	//private static bool run;

	void Start () {
		myTransform = transform;
	}


	void Update() {
		/*
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			run = true;
		}
		else
		{
			run = false;
		}
		*/

		//**************************************************************************************** moveDirection update (input):

		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
		if (moveDirection == Vector3.zero)
		{
			walking = false;
		}
		else 
		{
			walking = true;
		}


		//**************************************************************************************** isGrounded update: (teils aus: http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html)
		Ray ray;
		RaycastHit hit;
		ray = new Ray(myTransform.position, -myTransform.position); // direction of ray
		Physics.Raycast(ray, out hit); // cast ray downwards

		//Debug.Log ("hit.distance "+hit.distance);
		Debug.DrawLine (transform.position, hit.point, Color.cyan);

		if (hit.distance <= deltaGround)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		//Debug.Log ("isGrounded: "+isGrounded);

		//********************************************************************************************* Jump update (input):

		if (isGrounded)
		{
			doubleJump = false;
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded || doubleJump)
			{
				doubleJump = !doubleJump;
				jump = true;
				groundLevel = myTransform.position;
			}
		}
		//Debug.Log ("isGrounded: "+isGrounded);

	}

	void FixedUpdate() {
		if (moveDirection == Vector3.zero && isGrounded)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		else
		{
			// aus: https://www.youtube.com/watch?v=gHeQ8Hr92P4)
			rigidbody.MovePosition(myTransform.position + transform.TransformDirection(moveDirection)*moveSpeed*Time.deltaTime); //problem mit local moveDirection (drehen) anscheinend nicht mit local moveDirection sondern mit dem Attractor ein problem
		}

		if (jump && myTransform.rigidbody.velocity.magnitude <= 10)
		{
			rigidbody.AddForce(myTransform.position.normalized * jumpPower );
			//jumping = true;
			//jump = false;
			//StartCoroutine(JumpTimer());
			if (groundLevel.magnitude + jumpHeight <= myTransform.position.magnitude)
			{
				jump = false;
			}
		}

	}

	/*
	IEnumerator JumpTimer()
	{
		yield return new WaitForSeconds (1);
		jump = false;
	}
	*/
	Vector3 returnMoveDirection()
	{
		return moveDirection;
	}
	/*
	public static bool returnRun()
	{
		return run;
	}
	*/
}
