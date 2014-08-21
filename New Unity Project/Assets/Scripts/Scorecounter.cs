using UnityEngine;
using System.Collections;

public class Scorecounter : MonoBehaviour {
	
	public static int score = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void UpdateScore (int amount){
		score += amount*Highscore.multipler;
		//Debug.Log ("New Score: "+ score);
	}
	public static void ClearScore (){
		score = 0;
	}
}
