using UnityEngine;
using System.Collections;

public class Try4 : MonoBehaviour {

	bool isGrounded;
	bool falling = false; //is Character falling? sonst stoppt er wenn keine taste gedrückt wird

	float characterHeight = 3f; //anpassen, evt. berechnen und initaialisiern in Start() ************************************
	float gravity = 10f;
	float lerpSpeed = 10f;
	float speed = 30f;
	float speedMax = 20f;

	Vector3 myNormal; //character standing in that direction
	Vector3 surfaceNormal; //surface facing in that direction
	Vector3 myForward = Vector3.zero;

	Vector3 xDirection;
	Vector3 zDirection;



	void Start () {
		rigidbody.freezeRotation = true;
	}

	void FixedUpdate()
	{
		if (!isGrounded)
		{
			rigidbody.AddForce (-transform.position.normalized*gravity); //gravity
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			myForward = Vector3.Cross (xDirection, myNormal);
			rigidbody.AddForce (myForward.normalized*speed*Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			myForward = Vector3.Cross (myNormal, xDirection);
			rigidbody.AddForce (myForward.normalized*speed*Time.deltaTime);
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			myForward = Vector3.Cross (myNormal, zDirection);
			rigidbody.AddForce (myForward.normalized*speed*Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			myForward = Vector3.Cross (zDirection, myNormal);
			rigidbody.AddForce (myForward.normalized*speed*Time.deltaTime);
		}
		/*
		if(Input.anyKey == false)
		{
			if (!falling)
			{
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}
		}
		*/
	}
	

	void Update () {
		Ray ray;
		RaycastHit hit;
		myNormal = transform.position.normalized;

		xDirection = GameObject.Find("xDirection").transform.position;
		zDirection = GameObject.Find("zDirection").transform.position;



		ray = new Ray(transform.position, -myNormal); // cast ray towards (0/0/0)

		Physics.Raycast(ray, out hit);
		
		if (hit.distance <= characterHeight)
		{
			isGrounded = true;
			surfaceNormal = hit.normal;
		}
		else
		{
			isGrounded = false;
			//surfaceNormal = Vector3.up; 
		}


		// myForward bei InputCheck ändern, je nach richtung anderes Forward (später für Kisten Verschieben und Richtung der walk/idle Animation) *******************
		//var myForward = Vector3.Cross(transform.right, myNormal);

		// align character to the new myNormal while keeping the forward direction:
		//if (Input.anyKeyDown == true)
		//{
			Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);
		//}


		//Kamera zurückdrehen!

		//GameObject.Find("Main Camera").transform.RotateAround(transform.position, transform.position, 90f); //eine möglichkeit finden nur 1 mal ***************





		// Speedlimit (Bug wenn zu schnell)
		if (Vector3.Distance(rigidbody.velocity, Vector3.zero) >= speedMax)
		{
			rigidbody.velocity = rigidbody.velocity.normalized*speedMax;
		}

		// drehen mit xDirection und zDirection arbeiten? evt. besser mit Vector3.Cross(Vector a, Vector b) (aus myForward und transform.position) ***************
		// move the character forth/back with Vertical axis:
		//transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); 




		// Gravitation in move(), mit myNormal arbeiten (myNormal = transform.posiiton, vielleicht später ändern)
		// Jump evt. Doublejump aus RigidbodyCharacterController

	}
}
