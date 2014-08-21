using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {
	
	float velocityX = 0;
	float velocityZ = 0;
	public float speed = 5;
	public Vector3 moveDirection = Vector3.zero;
	CharacterController cameraController;
	public Vector3 playerPos = Vector3.zero;
	public Vector3 cameraPos = Vector3.zero;
	// Use this for initialization
	void Start () {
		cameraController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck ();
		Move ();
	}
	void InputCheck ()
	{
		velocityX = Input.GetAxis("Horizontal") * speed;
		velocityZ = Input.GetAxis ("Vertical") * speed;
	}
	void Move ()
	{
		moveDirection.z = velocityZ;
		moveDirection.x = velocityX;
		cameraController.Move (moveDirection*Time.deltaTime);
		moveDirection = Vector3.zero;
	}
}
