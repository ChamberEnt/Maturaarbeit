using UnityEngine;
using System.Collections;

public class FauxGravityAttractor : MonoBehaviour {

	public float gravity = -200;

	public void Attract(Transform body, bool isGrounded) 
	{
		// aus: https://www.youtube.com/watch?v=gHeQ8Hr92P4)
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.up;

		if (!isGrounded && body.rigidbody.velocity.magnitude <= 50)
		{
			body.rigidbody.AddForce(gravityUp * gravity);
		}

		Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation,targetRotation,50f * Time.deltaTime );
	}   

}
