using UnityEngine;
using System.Collections;

public class SendDamageCollider : MonoBehaviour {
	

	public int damageValue = 1;
	public string tagPlayer = "Player";
	public string tagWall = "Wall";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == tagPlayer)
		{
			other.gameObject.SendMessage("ApplyDamage", damageValue, SendMessageOptions.DontRequireReceiver);
		}
	}
}
