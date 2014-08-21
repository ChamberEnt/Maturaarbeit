using UnityEngine;
using System.Collections;

public class RigidbodyCharacterController : MonoBehaviour {

	float moveSpeed = 0.01f; // move speed
	float turnSpeed = 90f; // turning speed (degrees/second)
	float lerpSpeed = 10f; // smoothing speed
	float gravity = 0.2f; // gravity acceleration
	bool isGrounded;
	bool doubleJump = false;
	float deltaGround = 0.1f; // character is grounded up to this distance
	float distGround = 0.5f;
	float jumpSpeed = 10f; // vertical jump initial speed
	Vector3 xDirection;
	Vector3 zDirection;
	float jump = 0;
	float jmpPower = 5f;

	
	private Vector3 surfaceNormal; // current surface normal
	private Vector3 myNormal; // character normal
	//private float distGround; // distance from character position to ground
	private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
	private float vertSpeed = 0; // vertical jump current speed 
	
	void Start(){
		myNormal = transform.up; // normal starts as character up direction 
		rigidbody.freezeRotation = true; // disable physics rotation
		// distance from transform.position to ground
		//distGround = collider.bounds.extents.y - collider.center.y;
		//float distGround = 0.5f;

	}
	
	void FixedUpdate(){
		// apply constant weight force according to character normal:
		//rigidbody.AddForce(-gravity*rigidbody.mass*myNormal); //maybe just direction -transform.position
		//rigidbody.AddForce (-transform.position);
	}
	
	void Update(){
		// jump code - jump to wall or simple jump
		//if (jumping) return;  // abort Update while jumping to a wall


		xDirection = GameObject.Find("xDirection").transform.position;
		zDirection = GameObject.Find("zDirection").transform.position;

		Ray ray;
		RaycastHit hit;
		/*
		if (Input.GetButtonDown("Jump")){ // jump pressed:
			ray = Ray(transform.position, transform.forward);
			if (Physics.Raycast(ray, hit, jumpRange)){ // wall ahead?
				JumpToWall(hit.point, hit.normal); // yes: jump to the wall
			}
			else if (isGrounded){ // no: if grounded, jump up
				rigidbody.velocity += jumpSpeed * myNormal;
			}                
		}

		muss ich selber machen
		*/
		
		// movement code - turn left/right with Horizontal axis:
		//transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);
		// update surface normal and isGrounded:

		myNormal = transform.position;
		ray = new Ray(transform.position, -myNormal); // cast ray downwards

		Physics.Raycast(ray, out hit); // use it to update myNormal and isGrounded
	
		if (hit.distance <= distGround + deltaGround)
		{
			isGrounded = true;
			surfaceNormal = hit.normal;
		}
		else
		{
			isGrounded = false;
			// assume usual ground normal to avoid "falling forever"
			surfaceNormal = Vector3.up; 
		}

		if (isGrounded)
		{
			doubleJump = false;
		}

		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded || doubleJump)
			{
				doubleJump = !doubleJump;
				jump = jmpPower;
			}
		}

		move ();

		//myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
		// find forward direction with new myNormal:

		//var myForward = Vector3.Cross(transform.right, myNormal);
		// align character to the new myNormal while keeping the forward direction:

		//var targetRot = Quaternion.LookRotation(myForward, myNormal);
		//transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);
		// move the character forth/back with Vertical axis:

		//transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); 
	}
	void move()
	{
		//transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime);
		//transform.Translate(Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime, 0, 0);

		transform.RotateAround(Vector3.zero, xDirection, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime);
		transform.RotateAround(Vector3.zero, zDirection, Input.GetAxis("Horizontal")*-moveSpeed*Time.deltaTime);

		/*
		if (0 != Input.GetAxis("Vertical"))
		{
			rigidbody.AddForce(xDirection*moveSpeed);
		}
		if (0 != Input.GetAxis("Horizontal"))
		{
			rigidbody.AddForce(zDirection*moveSpeed*);
		}
		*/



		if (jump > 0)
		{
			transform.Translate (transform.position*jump*Time.deltaTime);
			jump -= gravity;
		}

		else if (!isGrounded)
		{
			transform.Translate (transform.position*gravity*Time.deltaTime);
		}

	}

}