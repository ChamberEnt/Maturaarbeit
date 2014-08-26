using UnityEngine;
using System.Collections;

public class DirectionIndicatorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerControllerFauxGravity.moveDirection != Vector3.zero)
		{
			transform.localPosition = PlayerControllerFauxGravity.moveDirection;
		}
	}
}