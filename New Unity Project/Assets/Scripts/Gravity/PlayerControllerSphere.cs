using UnityEngine;
using System.Collections;

public class PlayerControllerSphere : MonoBehaviour {

	float velocityX = 0;
	float velocityZ = 0;
	bool doubleJump = false;
	public float speed = 5;
	public float jmpPower = 10;
	bool inputJump = false;
	public Vector3 moveDirection = Vector3.zero;
	CharacterController characterController;
	public float gravity = 1;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController.isGrounded && !HealthController.damageEffect)
		{
			doubleJump = false;
		}
		InputCheck ();
		Move ();
	}
	void InputCheck ()
	{
		velocityX = Input.GetAxis("Horizontal") * speed;
		velocityZ = Input.GetAxis("Vertical") * speed;
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
		if  (inputJump || doubleJump){
			doubleJump = false;
			moveDirection.y = jmpPower;
		}

			
		moveDirection.z = velocityZ;
		moveDirection.x = velocityX;
		//moveDirection.y -= gravity;
		characterController.Move (moveDirection*Time.deltaTime);
	}
}
