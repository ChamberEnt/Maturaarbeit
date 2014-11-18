using UnityEngine;
using System.Collections;

public class DoorTeleporter : MonoBehaviour {
	public Vector3 newPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("Teleport", newPos, SendMessageOptions.DontRequireReceiver);
			//eventuell noch rotation des Charakters anpassen, dass er "aus" der Türe kommt.
		}
	}
}
