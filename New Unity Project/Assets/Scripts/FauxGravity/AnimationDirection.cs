using UnityEngine;
using System.Collections;

public class AnimationDirection : MonoBehaviour {
	
	private float degreesRotated = 0;
	private float degreesToRotate = 0;
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		//myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerControllerFauxGravity.moveDirection != Vector3.zero)
		{
			if (Input.GetAxisRaw("Horizontal") == 1)
			{
				degreesToRotate = 270;
			}
			else if (Input.GetAxisRaw("Horizontal") == -1)
			{
				degreesToRotate = 90;
			}

			if (Input.GetAxisRaw("Vertical") == 1)
			{
				if (degreesToRotate == 0)
				{
					degreesToRotate = 360;
				}
				else if (degreesToRotate == 90)
				{
					degreesToRotate = 45;
				}
				else if (degreesToRotate == 270)
				{
					degreesToRotate = 315;
				}
			}
			else if (Input.GetAxisRaw("Vertical") == -1)
			{
				if (degreesToRotate == 0)
				{
					degreesToRotate = 180;
				}
				else if (degreesToRotate == 90)
				{
					degreesToRotate = 135;
				}
				else if (degreesToRotate == 270)
				{
					degreesToRotate = 225;
				}
			}

			transform.RotateAround(transform.position, transform.position, degreesRotated - degreesToRotate);

			degreesRotated = degreesToRotate;
			degreesToRotate = 0;

		}
	}
}
