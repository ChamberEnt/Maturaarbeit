using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour {
	
	public static bool startBool; // um bewegung des Gegners zu starten auf true setzen
	public float moveSpeed; //Bewegunsgeschwindigkeit
	public float turnSpeed; //Drehgeschwindigkeit (in s/90Grad )
	public FauxGravityAttractor attractor; //Planet/Level
	public static Transform myTransform; //Position + Rotation + Grösse
	private bool isGrounded; //ob das Objekt den Boden berührt
	private bool moving; //ob das Objekt sich fortbewegt
	private bool turning; //ob das Objekt sich dreht
	private float[] walkingPath = new float[4]; //Liste mit den Laufdistanzen
	private float[] turningPath = new float[4]; //Liste mit den Drehwinkeln
	private int patroullienZaehler = 0; //Zähler um durch die oberen Listen zugehen
	private Vector3 startPos; //Anfangsposition
	private Quaternion startRot; //Anfangsrotation
	private Vector3 oldPosition; //Position an der das letzte mahl gedreht wurde
	private Quaternion oldRotation; //Rotation an der das erste mahl gelaufen wurde

	/*
	void Awake()
	{
		Application.targetFrameRate(60);
	}
	*/

	void Start () {
		startBool = false;
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
		/*
		float abcd = 1.0f / Time.deltaTime;
		Debug.Log (""+abcd);
		*/

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

		Debug.DrawLine(myTransform.position, myTransform.position + new Vector3(0 ,0.1f ,0), Color.black, 50);

		if (moving)
		{
			if (Vector3.SqrMagnitude(oldPosition - myTransform.position) <= (3.95f/2)*moveSpeed) //ACHTUNG 3.95f müssen angepasst werden!!! ist die distanz, irgendwie aus moveSpeed und walkTime (also walkingPath[patroullienZaehler]) berechenen
			{
				rigidbody.MovePosition(myTransform.position + myTransform.forward.normalized * moveSpeed * Time.deltaTime);
			}
		}
		
		if (turning)
		{
			if(Quaternion.Angle(myTransform.rotation, oldRotation) < turningPath[patroullienZaehler])
			{
				if (turningPath[patroullienZaehler] > 180)
				{
					if(Quaternion.Angle(myTransform.rotation, oldRotation) <= 360 - turningPath[patroullienZaehler])
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
		oldPosition = myTransform.position;
		moving = true;
		yield return new WaitForSeconds(walkTime);
		moving = false;

		if(turningPath[patroullienZaehler] > 180)
		{
			angleTemp = 360-turningPath[patroullienZaehler];
		}
		else
		{
			angleTemp = turningPath[patroullienZaehler];
		}
		StartCoroutine(TurnTimer (angleTemp));
	}
	

	IEnumerator TurnTimer(float angle)
	{
		oldRotation = myTransform.rotation;
		turning = true;
		yield return new WaitForSeconds(angle/(90f*turnSpeed));
		turning = false;
		patroullienZaehler =  (patroullienZaehler+1)%(walkingPath.Length);

		if (patroullienZaehler == 0)
		{
			myTransform.position = startPos;
			myTransform.rotation = startRot;
		}

		StartCoroutine(WalkTimer (walkingPath[patroullienZaehler]));
	}

	public void SetStartBool(bool sB)
	{
		startBool = sB;
	}
	

}
