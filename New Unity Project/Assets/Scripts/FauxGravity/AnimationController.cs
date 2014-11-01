using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	//private AnimationState idle;
	//private AnimationState walk;

	void Start () {
		//idle = animation["idle1"];
		//walk = animation["walk3"];

		animation.wrapMode = WrapMode.Loop;

		//setup für jump, wrapMode nur Once und layer 1 damit es walk3 und idle1 nicht unterbricht
		//animation["jump"].wrapMode = WrapMode.Once;
		//animation["jump"].layer = 1;

	}
	
	// Update is called once per frame
	void Update () {
		animation.CrossFade("idle1");

		if (PlayerControllerFauxGravity.walking)
		{
			animation.Play("walk3");
		}
	}
}
