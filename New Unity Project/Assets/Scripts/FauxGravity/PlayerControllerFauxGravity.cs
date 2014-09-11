﻿using UnityEngine;
using System.Collections;

public class PlayerControllerFauxGravity : MonoBehaviour {

	private float moveSpeed = 15;
	public static Vector3 moveDirection;
	private Transform myTransform;
	public float charHeight = 0;
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


		//**************************************************************************************** isGrounded update: (teils aus: http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html)
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
			rigidbody.MovePosition(myTransform.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime); //problem mit local moveDirection (drehen) anscheinend nicht mit local moveDirection sondern mit dem Attractor ein problem
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
	Vector3 returnMoveDirection()
	{
		return moveDirection;
	}
	
}
