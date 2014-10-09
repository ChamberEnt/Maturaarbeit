using UnityEngine;
using System.Collections;

public class AnimationDirection : MonoBehaviour {
	
	//private float degreesRotated = 0;
	private float degreesToRotate = 0;
	public float turnSpeed;
	private Vector3 myPos;
	//new Transform myTrans;

	// Use this for initialization
	void Start () {
		//myTrans = transform;
	}
	
	// Update is called once per frame
	void Update () {


		if (PlayerControllerFauxGravity.moveDirection != Vector3.zero)
		{
			Quaternion targetRotation = transform.localRotation;
			if (Input.GetAxisRaw("Horizontal") == 1)
			{
				//Debug.Log("Right");
				degreesToRotate = 270;
				targetRotation = new Quaternion(0, -0.7f, 0, -0.7f);
			}
			else if (Input.GetAxisRaw("Horizontal") == -1)
			{
				//Debug.Log("Left");
				degreesToRotate = 90;
				targetRotation = new Quaternion(0, -0.7f, 0, 0.7f);
			}

			if (Input.GetAxisRaw("Vertical") == 1)
			{
				if (degreesToRotate == 0)
				{
					//Debug.Log("Up");
					degreesToRotate = 360;
					targetRotation = new Quaternion(0, 0, 0, -1f);
				}
				else if (degreesToRotate == 90)
				{
					//Debug.Log("Up + Left");
					degreesToRotate = 45;
					targetRotation = new Quaternion(0, -0.4f, 0, 0.9f);
				}
				else if (degreesToRotate == 270)
				{
					//Debug.Log("Up + Right");
					degreesToRotate = 315;
					targetRotation = new Quaternion(0, -0.4f, 0, -0.9f);
				}
			}
			else if (Input.GetAxisRaw("Vertical") == -1)
			{
				if (degreesToRotate == 0)
				{
					//Debug.Log("Down");
					degreesToRotate = 180;
					targetRotation = new Quaternion(0, -1f, 0, 0);
				}
				else if (degreesToRotate == 90)
				{
					//Debug.Log("Down + Left");
					degreesToRotate = 135;
					targetRotation = new Quaternion(0, -0.9f, 0, 0.4f);
				}
				else if (degreesToRotate == 270)
				{
					//Debug.Log("Down + Right");
					degreesToRotate = 225;
					targetRotation = new Quaternion(0, -0.9f, 0, -0.4f);
				}
			}

			//float roateABCD = degreesRotated - degreesToRotate;
			//Debug.Log(""+roateABCD);
			// Quaternion targetRotation = new Quaternion(0, degreesRotated - degreesToRotate, 0, transform.localRotation.w);
			if (targetRotation != transform.localRotation)
			{
				transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, turnSpeed * Time.deltaTime );
			}
			//Debug.Log(""+transform.localRotation);

			/*
			//funktioniert, aber keine Drehzeit, sieht abgehackt aus...
			myPos = GameObject.Find ("CharacterCollider").transform.position;
			transform.RotateAround(myPos , myPos, degreesRotated - degreesToRotate);
			transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
			*/

			//transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0, myTrans.localRotation.y, 0, myTrans.localRotation.w), turnSpeed*Time.deltaTime);

			//myTransformToRotate.RotateAround(transform.position, transform.position, degreesRotated - degreesToRotate); //muss local sein!

			//transform.localRotation = Quaternion.RotateTowards(myTransform.localRotation, myTransformToRotate.localRotation, 0.0001f*Time.deltaTime);

			//myTransform = transform;
			//myTransformToRotate = myTransform

			//Debug.Log (""+PlayerControllerFauxGravity.moveDirection);
			//Quaternion.LookRotation (transform.TransformDirection(PlayerControllerFauxGravity.moveDirection), myTransform.position)

			//myTransformToRotate.localRotation = 

			//transform.localRotation = Quaternion.Slerp (myTransform.localRotation, , 25*Time.deltaTime);

			//Quaternion targetRotation = Quaternion.LookRotation(PlayerControllerFauxGravity.moveDirection, myTransform.position) * myTransform.rotation;
			//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, 25f * Time.deltaTime);


			//degreesRotated = degreesToRotate;
			degreesToRotate = 0;

			/*
			if (PlayerControllerFauxGravity.returnRun())
			{
				transform.RotateAround(transform.position, transform.TransformDirection(new Vector3(0,1,0)), 180*Time.deltaTime);
			}
			*/

		}
	}
}
