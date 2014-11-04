using UnityEngine;
using System.Collections;

public class EnemyAwareness : MonoBehaviour {

	public static float fieldOfViewDegrees; //Sichtfeld in Grad
	public static float visibilityDistance; //Sichtweite
	private static Transform myTransform; //Position + Rotation + Grösse


	// Use this for initialization
	void Start () {
		myTransform = transform;
		fieldOfViewDegrees = 90;
		visibilityDistance = 7;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localForward = transform.forward*visibilityDistance+transform.position;
		Debug.DrawLine(transform.position, localForward, Color.green);
		Debug.Log ("CanSeePlayer: "+CanSeePlayer());
		CanSeePlayer ();
	}

	public static bool CanSeePlayer()
	{
		GameObject Player = GameObject.Find("Player");
		RaycastHit hit;
		Vector3 rayDirection = Player.transform.position - myTransform.transform.position;



		if ((Vector3.Angle(rayDirection, myTransform.forward)) <= fieldOfViewDegrees * 0.5f)
		{

			if (Physics.Raycast(myTransform.position, rayDirection, out hit, visibilityDistance))
			{
				Debug.DrawLine(myTransform.position, hit.point, Color.red);
				return (hit.transform.CompareTag("Player"));
			}
		}
		return false;
	}
}
