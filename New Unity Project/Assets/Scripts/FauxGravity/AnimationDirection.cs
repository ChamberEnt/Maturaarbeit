using UnityEngine;
using System.Collections;

public class AnimationDirection : MonoBehaviour {

	private Vector3 facingDirection;
	Transform Direction;

	Vector3 lookAt;
	// Use this for initialization
	void Start () {
		facingDirection = new Vector3(0,0,1);
		Direction = GameObject.Find("DirectionIndicator").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerControllerFauxGravity.moveDirection != Vector3.zero)
		{
			//Quaternion targetRotation = Quaternion.FromToRotation(facingDirection,PlayerControllerFauxGravity.moveDirection);
			//transform.localRotation = Quaternion.Slerp(transform.rotation,targetRotation, 50f * Time.deltaTime );
			//facingDirection = PlayerControllerFauxGravity.moveDirection;
			//Debug.Log ("lR: "+transform.localRotation);
			//Quaternion targetRotation = Quaternion.LookRotation(facingDirection, transform.position);
			//Debug.Log ("fD, t.p: "+facingDirection+", "+transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, 50f * Time.deltaTime );

			transform.LookAt(Direction);
		}
	}
}
