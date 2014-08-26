using UnityEngine;
using System.Collections;

public class PlayerControllerFauxGravity : MonoBehaviour {

	private float moveSpeed = 15;
	public static Vector3 moveDirection;
	private Transform myTransform;
	private float charHeight = 1.1f;
	public static bool isGrounded;
	private bool doubleJump = false;
	private bool jump = false;
	private float jumpPower = 250;
	public static bool jumping = false;

	void Start () {
		myTransform = transform;
	}


	void Update() {


		//**************************************************************************************** moveDirection update (input):
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;


		//**************************************************************************************** isGrounded update:
		Ray ray;
		RaycastHit hit;
		ray = new Ray(myTransform.position, -myTransform.position); // direction of ray
		Physics.Raycast(ray, out hit); // cast ray downwards
		if (hit.distance <= charHeight)
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
			}
		}
		//Debug.Log ("myTransform.position: "+myTransform.position);
	}

	void FixedUpdate() {
		if (moveDirection == Vector3.zero && isGrounded)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		else
		{
			rigidbody.MovePosition(myTransform.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
		}

		if (jump)
		{
			rigidbody.AddForce(myTransform.position.normalized * jumpPower );
			//jumping = true;
			jump = false;
			//StartCoroutine(JumpTimer());
		}

	}

	IEnumerator JumpTimer()
	{
		yield return new WaitForSeconds (1);
		jumping = false;
	}
	
}
