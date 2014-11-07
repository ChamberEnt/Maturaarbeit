using UnityEngine;
using System.Collections;

public class ObjectTeleport : MonoBehaviour {
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Teleport(Vector3 newPos)
	{
		myTransform.position = newPos;
	}
}
