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
		Debug.Log ("CanSeePlayer: "+CanSeePlayer());
	}
	protected bool CanSeePlayer()
	{
		GameObject Player = GameObject.Find("PositionIndicator");
		RaycastHit hit;
		Vector3 rayDirection = Player.transform.position - transform.position;

		Vector3 localForward = transform.forward*5+transform.position;
		Debug.DrawLine(transform.position, localForward, Color.green);

		if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
		{
			// Detect if player is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
			{
				Debug.DrawLine(transform.position, hit.point, Color.red, 1);
				return (hit.transform.CompareTag("Player"));
			}
		}
		
		return false;
	}
}
