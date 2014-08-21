using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	System.Random rand = new System.Random();
	Vector3 moveDirection = Vector3.zero;
	float velocityZ = 0;
	float velocityX = 0;
	Vector3 start = Vector3.zero;
	float difficulty = enemy1.difficultyMultiplier*10;
	
	// Use this for initialization
	void Start () {
		
		
		
		 switch (rand.Next(3))
        {
            case 0:
				start.x = -44;
				start.y = 0.25f;
				start.z = (float) rand.Next(40)-20;
				velocityX = 1;
				break;
            case 1:
				start.x = 44;
				start.y = 0.25f;
				start.z = (float) rand.Next(40)-20;
				velocityX = -1;
                break;
            case 2:
				start.z = -19;
				start.y = 0.25f;
				start.x = (float) rand.Next(92)-46;
				velocityZ = 1;
                break;
            case 3:
               	start.z = 19;
				start.y = 0.25f;
				start.x = (float) rand.Next(92)-46;
				velocityZ = -1;
                break;
            default:
                Debug.Log ("Shit happens...");
                break;
        }
		
		transform.position = start;
		/*if (rand.Next(0,100) < 50)
		{
			velocityZ = (float) rand.NextDouble()*10;
		}
		else
		{
			velocityZ = 0-((float) rand.NextDouble()*10);
		}
		if (rand.Next(0,100) < 50)
		{
			velocityX = (float) rand.NextDouble()*10;
		}
		else
		{
			velocityX = 0-((float) rand.NextDouble()*10);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		move();
	}
	void move()
	{
		moveDirection.z = velocityZ*difficulty;
		moveDirection.x = velocityX*difficulty;
		transform.Translate(moveDirection*Time.deltaTime);
	}
}
