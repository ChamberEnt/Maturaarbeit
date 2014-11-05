using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float degreesToRotate;
	public float turnSpeed;
	private bool switcher;
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		switcher = false;
		StartCoroutine (SwitcherTimer());
	}
	
	// Update is called once per frame
	void Update () {
		if (switcher)
			transform.RotateAround(myTransform.position, -myTransform.position, turnSpeed * Time.deltaTime * 90f);
		else
			transform.RotateAround(myTransform.position, myTransform.position, turnSpeed * Time.deltaTime * 90f);
	}

	IEnumerator SwitcherTimer ()
	{
		yield return new WaitForSeconds(degreesToRotate/(90f*turnSpeed));
		switcher = !switcher;
		StartCoroutine (SwitcherTimer());
	}
}
