using UnityEngine;
using System.Collections;

public class enemy1 : MonoBehaviour {
	
	public GameObject blueBox;
	System.Random rand = new System.Random();
	static public float difficultyMultiplier = 4;

	void Start () {
		Highscore.SetDifficulty((int)difficultyMultiplier);
		Spawn();
	}
	
	
	void Update () {
		if (rand.NextDouble()<0.02*difficultyMultiplier){
			Spawn ();
		}
	}
	
	void Spawn (){
		Instantiate(blueBox);
	}
}
