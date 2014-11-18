using UnityEngine;
using System.Collections;

public class MenuGameOver : MonoBehaviour {


	void OnGUI()
	{
		GUI.Label (new Rect(20,20,150,50),"Don't punch the Screen! But we have to tell you, you are dead...");

		if (GUI.Button (new Rect(20,90,150,50),"Retry"))
		{
			Application.LoadLevel(1);
		}
		
		if (GUI.Button (new Rect(20,160,150,50),"Menu"))
		{
			Application.LoadLevel(0);
		}
	}
}
