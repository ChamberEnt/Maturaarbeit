using UnityEngine;
using System.Collections;

public class PlayerControllerCube : MonoBehaviour {

	float velocityX = 0;
	float velocityZ = 0;
	bool doubleJump = false;
	float jump = 0;
	public float speed = 2;
	public float jmpPower = 10;
	bool inputJump = false;
	public Vector3 moveDirection = Vector3.zero;
	CharacterController characterController;
	public float gravity = 1;
	Vector3 xDirection; // new Vector3(GameObject.Find("xDirection").transform.position.x,GameObject.Find("xDirection").transform.position.y,GameObject.Find("xDirection").transform.position.z);
	Vector3 zDirection;
	Vector3 gravityV;

	
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController.isGrounded)
		{
			doubleJump = false;
		}
		InputCheck ();
		Move ();
	}
	void InputCheck ()
	{
		xDirection = GameObject.Find("xDirection").transform.position;
		zDirection = GameObject.Find("zDirection").transform.position;

		velocityX = Input.GetAxis("Vertical") * speed;
		velocityZ = Input.GetAxis("Horizontal") * -speed;
		if (Input.GetKeyDown (KeyCode.Space))
		{
			inputJump = true;
		}
		else
		{
			inputJump = false;
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			//Menu
		}
	}
	void Move ()
	{
		if (inputJump){
			if  (characterController.isGrounded || doubleJump){
				doubleJump = !doubleJump;
				jump = jmpPower;
			}
		}

		//Vector3.MoveTowards(current, target, maxDelta);
		//gravityV = -Vector3.MoveTowards(transform.position, Vector3.zero, 5f);


		//zDirection = Vector3.MoveTowards(transform.position, zDirection, velocityZ);


		//moveDirection.z = velocityZ;
		//moveDirection.x = velocityX;


		//moveDirection.y -= gravity;

		//characterController.Move (moveDirection*Time.deltaTime);
		/*
		if (jump > 0)
		{
			characterController.Move (transform.position*jump*Time.deltaTime);
			jump -= gravity;
		}
		else 
		{
			characterController.Move (-transform.position*Time.deltaTime);
		}
		*/




		transform.RotateAround(Vector3.zero, xDirection, 1f*velocityX);
		transform.RotateAround(Vector3.zero, zDirection, 1f*velocityZ);
		//evt. eigenen Charactercontroller coden für Boxcollider...

	}
}
