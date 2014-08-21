using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	
	public float animationTime = 1;
	public Material playerMat;
	public Material playerMatJump;
	public float gravity = 1;
	float velocityX = 0;
	float velocityZ = 0;
	bool doubleJump = false;
	public float speed = 5;
	public float jmpPower = 10;
	bool inputJump = false;
	public Vector3 moveDirection = Vector3.zero;
	CharacterController characterController;
	bool checkAnimation = false;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController.isGrounded && !HealthController.damageEffect)
		{
			renderer.material = playerMat;
			doubleJump = false;
		}
		InputCheck ();
		Move ();
	}
	void InputCheck ()
	{
		velocityX = Input.GetAxis("Horizontal") * speed;
		velocityZ = Input.GetAxis("Vertical") * speed;
		if (Input.GetKeyDown (KeyCode.Space))
		{
			inputJump = true;
		}
		else
		{
			inputJump = false;
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			Scorecounter.ClearScore();
			Application.LoadLevel(0);
		}
	}
	void Move ()
	{
		if (inputJump){
			if  (characterController.isGrounded || doubleJump){
				doubleJump = !doubleJump;
				moveDirection.y = jmpPower;
				renderer.material = playerMatJump;
			}
		}
		
		if (transform.position.x >= 29){
			if (velocityX > 0){
				velocityX = 0;
			}
		}
		if (transform.position.x <= -29){
			if (velocityX < 0){
				velocityX = 0;
			}
		}
		if (transform.position.z >= 16){
			if (velocityZ > 0){
				velocityZ = 0;
			}
		}
		if (transform.position.z <= -16){
			if (velocityZ < 0){
				velocityZ = 0;
			}
		}
		if (velocityZ != 0 && !checkAnimation)
		{
			StartCoroutine(MoveAnimation());
		}
		else if (!checkAnimation)
		{
			animation.Stop();
		}

		moveDirection.z = velocityZ;
		moveDirection.x = velocityX;
		moveDirection.y -= gravity;
		characterController.Move (moveDirection*Time.deltaTime);
	}

	IEnumerator MoveAnimation ()
	{
		checkAnimation = true;

		animation.Play();
		yield return new WaitForSeconds(animationTime);
		checkAnimation = false;
	}
}
