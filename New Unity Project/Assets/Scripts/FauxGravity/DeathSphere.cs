using UnityEngine;
using System.Collections;

public class DeathSphere : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
		}
	}
}
