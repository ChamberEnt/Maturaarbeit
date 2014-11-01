using UnityEngine;
using System.Collections;

public class DirectionIndicatorScript : MonoBehaviour {

	//Unwichtiges Script, könnte gelöscht werden

	Vector3 offset = new Vector3(0,1,0);

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerControllerFauxGravity.moveDirection != Vector3.zero)
		{
			transform.localPosition = PlayerControllerFauxGravity.moveDirection * 10 - offset;
			//if (PlayerControllerFauxGravity.moveDirection != transform.localPosition)
			{	
				/*
				switch (switchExpression)
				{
					// A switch section can have more than one case label. 
				case 0:
				case 1:
					Console.WriteLine("Case 0 or 1");
					// Most switch sections contain a jump statement, such as 
					// a break, goto, or return. The end of the statement list 
					// must be unreachable. 
					break;
				}
				*/
			}
		}
	}
}