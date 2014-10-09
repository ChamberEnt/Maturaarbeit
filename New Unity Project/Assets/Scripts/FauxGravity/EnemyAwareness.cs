using UnityEngine;
using System.Collections;

public class EnemyAwareness : MonoBehaviour {

	public float fieldOfViewDegrees;
	public float visibilityDistance;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localForward = transform.forward*visibilityDistance+transform.position;
		Debug.DrawLine(transform.position, localForward, Color.green);
		//Debug.Log ("CanSeePlayer: "+CanSeePlayer());
		CanSeePlayer ();
	}

	bool CanSeePlayer()
	{
		GameObject Player = GameObject.Find("PositionIndicator");
		RaycastHit hit;
		Vector3 rayDirection = Player.transform.position - transform.position;



		if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
		{

			if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
			{
				Debug.DrawLine(transform.position, hit.point, Color.red);
				return (hit.transform.CompareTag("Player"));
			}
		}
		
		return false;
	}
}
