using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour {
	
	public bool startBool = false;
	public float moveSpeed;
	public float turnSpeed;
	public FauxGravityAttractor attractor;
	public static Transform myTransform;
	private bool isGrounded;
	private bool moving;
	private bool turning;
	private float[] walkingPath = new float[4];
	private float[] turningPath = new float[4];
	private int i = 0;
	private Vector3 startPos;
	private Quaternion startRot;
	private Vector3 x;
	private Quaternion y;

	/*
	void Awake()
	{
		Application.targetFrameRate(60);
	}
	*/

	void Start () {
		turning = false;
		moving = false;

		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		myTransform = transform;
		//StartCoroutine(WalkTimer (2));
		//StartCoroutine(TurnTimer (180));
		walkingPath[0] = 1;
		walkingPath[1] = 1;
		walkingPath[2] = 1;
		walkingPath[3] = 1;


		turningPath[0] = 90;
		turningPath[1] = 180;
		turningPath[2] = 270;
		turningPath[3] = 180;



		//StartCoroutine (WalkTimer (walkingPath[0]));


	}

	void Update () {

		float abcd = 1.0f / Time.deltaTime;
		Debug.Log (""+abcd);

		if (startBool)
		{
			startPos = myTransform.position;
			startRot = myTransform.rotation;
			StartCoroutine (WalkTimer (walkingPath[0]));
		}




	}

	void FixedUpdate () {

		Ray ray;
		RaycastHit hit;
		ray = new Ray(myTransform.position, -myTransform.position); // direction of ray
		Physics.Raycast(ray, out hit); // cast ray downwards
		
		//Debug.Log ("hit.distance "+hit.distance);
		Debug.DrawLine (transform.position, hit.point, Color.black);
		
		if (hit.distance <= 1f) //MaxAbstand Boden-Körper
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		//Debug.Log ("isGrounded: "+isGrounded);


		//aus: https://www.youtube.com/watch?v=gHeQ8Hr92P4)
		if (attractor)
		{
			attractor.Attract(myTransform, isGrounded);
		}

		if (!moving && isGrounded)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}

		//Debug.DrawLine(myTransform.position, myTransform.position + new Vector3(0 ,0.1f ,0), Color.black, 50);

		if (moving)
		{
			if (Vector3.SqrMagnitude(x-myTransform.position) <= (3.95f/2)*moveSpeed) //ACHTUNG 3.95f müssen angepasst werden!!! ist die distanz, irgendwie aus moceSpeed und walkTime (also walkingPath[i]) berechenen
			{
				rigidbody.MovePosition(myTransform.position + myTransform.forward.normalized * moveSpeed * Time.deltaTime);
			}
		}
		
		if (turning)
		{
			if(Quaternion.Angle(myTransform.rotation, y) < turningPath[i])
			{
				if (turningPath[i] > 180)
				{
					if(Quaternion.Angle(myTransform.rotation, y) <= 360 - turningPath[i])
					{
						transform.RotateAround(myTransform.position, -myTransform.position, turnSpeed * Time.deltaTime * 90f);
						//Debug.Log (""+Quaternion.Angle(myTransform.rotation, y));
					}
				}
				else
				{
					transform.RotateAround(myTransform.position, myTransform.position, turnSpeed * Time.deltaTime * 90f);
					//Debug.Log (""+Quaternion.Angle(myTransform.rotation, y));
				}
			}
		}
		
	}
	

	IEnumerator WalkTimer(float walkTime)
	{
		float angleTemp;
		startBool = false;
		x = myTransform.position;
		moving = true;
		yield return new WaitForSeconds(walkTime);
		moving = false;

		if(turningPath[i] > 180)
		{
			angleTemp = 360-turningPath[i];
		}
		else
		{
			angleTemp = turningPath[i];
		}
		StartCoroutine(TurnTimer (angleTemp));
	}
	

	IEnumerator TurnTimer(float angle)
	{
		y = myTransform.rotation;
		turning = true;
		yield return new WaitForSeconds(angle/(90f*turnSpeed));
		turning = false;
		i =  (i+1)%(walkingPath.Length);

		if (i == 0)
		{
			myTransform.position = startPos;
			myTransform.rotation = startRot;
		}

		StartCoroutine(WalkTimer (walkingPath[i]));
	}
	

}
