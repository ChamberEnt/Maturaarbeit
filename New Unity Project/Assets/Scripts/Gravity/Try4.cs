using UnityEngine;
using System.Collections;

public class Try4 : MonoBehaviour {

	bool isGrounded;

	float characterHeight = 1f; //anpassen, evt. berechnen und initaialisiern in Start() ************************************
	float gravity = 10f;
	float lerpSpeed = 10f;

	Vector3 myNormal; //character standing in that direction
	Vector3 surfaceNormal; //surface facing in that direction
	Vector3 myForward = Vector3.zero;



	void Start () {
	
	}

	void FixedUpdate()
	{
		rigidbody.AddForce (-transform.position.normalized*gravity);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray;
		RaycastHit hit;

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
		var targetRot = Quaternion.LookRotation(myForward, myNormal);

		transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);


		// mit xDirection und zDirection arbeiten? evt. besser mit Vector3.Cross(Vector a, Vector b) (aus myForward und transform.position) ***************
		// move the character forth/back with Vertical axis:
		//transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); 




		// Gravitation in move(), mit myNormal arbeiten (myNormal = transform.posiiton, vielleicht später ändern)
		// Jump evt. Doublejump aus RigidbodyCharacterController

	}
}
