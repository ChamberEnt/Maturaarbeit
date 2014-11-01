using UnityEngine;
using System.Collections;

public class MenuIntro : MonoBehaviour {
	
	//int highscore = Highscore.highscore;
	
	void OnGUI()
	{
		if (GUI.Button (new Rect(20,20,150,50),"Start"))
		{
			//Scorecounter.ClearScore();
			Application.LoadLevel(1);
		}
		/*
		if (GUI.Button (new Rect(170,10,150,50),"Start Walking Legs"))
		{
			Application.LoadLevel(2);
		}
		*/
		//GUI.Label (new Rect(10,70,100,50),"Highscore: "+highscore);

		if (GUI.Button (new Rect(20,90,150,50),"Exit"))
		{
			Application.Quit();
		}
	}
}
