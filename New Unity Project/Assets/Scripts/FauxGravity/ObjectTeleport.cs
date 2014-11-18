using UnityEngine;
using System.Collections;

public class ObjectTeleport : MonoBehaviour {
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}

	void Teleport(Vector3 newPos)
	{
		myTransform.position = newPos;
		//eventuell noch rotation des Charakters anpassen, dass er "aus" der Türe kommt.
	}
}
