using UnityEngine;
using System.Collections;

public class healthControllerEnemy : MonoBehaviour {
	
	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy (gameObject);
			if (other.gameObject.transform.position.y > 1.531){
				Scorecounter.UpdateScore(1);
			}
		}
		else if (other.gameObject.tag == "Wall")
		{
			//Debug.Log ("Poop");
			Destroy (gameObject);
			//HighscoreManager.score += 1;
			//HighscoreManager.UpdateScore();
		}
	}
}
