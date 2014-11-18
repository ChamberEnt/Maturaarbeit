﻿using UnityEngine;
using System.Collections;

public class PlayerControllerFauxGravity : MonoBehaviour {

	public float moveSpeed; //Bewegungsgeschwindigkeit
	public static Vector3 moveDirection; //Laufrichtung (vom Spieler aus gesehen, ohne Drehung)
	private Transform myTransform; //Position + Rotation + Grösse
	public float deltaGround; //Gibt an wie weit der abstand zwischen Boden und Objekt sein kann ohne das das Objekt nicht am Boden steht, wichtig bei schrägen Flächen
	public static bool isGrounded; //ob das Objekt den Boden berührt

	//Das ganze Junping sollte noch überarbeitet werden, wenn zeit besteht
	
	private bool jump;
	public float jumpPower;
	public static bool jumping = false;
	public static bool walking = false;
	private Vector3 groundLevel;
	//private static bool run;

	void Start () {
		myTransform = transform;
		jump = false;
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

		
		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded)
			{
				jump = true;
				groundLevel = myTransform.position;
			}
		}
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
			rigidbody.MovePosition(myTransform.position + transform.TransformDirection(moveDirection)*moveSpeed*Time.deltaTime); //problem mit local moveDirection (drehen) anscheinend nicht mit local moveDirection sondern mit dem Attractor ein problem EDIT: wut?
		}

		//if (jump && myTransform.rigidbody.velocity.magnitude <= 10)
		if (jump)
		{
			rigidbody.AddForce(myTransform.position.normalized * jumpPower );
			jumping = true;
			StartCoroutine(JumpTimer());
			jump = false;
		}
//		else if (jumping && isGrounded)
//		{
//			jumping = false;
//		}
		//Debug.Log ("jumping: "+jumping);

	}

	IEnumerator JumpTimer()
	{
		yield return new WaitForSeconds (0.25f);
		jumping = false;
	}

	void GameOver()
	{
		Application.LoadLevel(2);
	}
	
	public Vector3 returnMoveDirection()
	{
		return moveDirection;
	}
	public static bool returnJumping()
	{
		return jumping;
	}
	
	/*
	public static bool returnRun()
	{
		return run;
	}
	*/
}
