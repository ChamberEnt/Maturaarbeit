using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public float currentHealth = 3;
	public float damageEffectTime = 0.02F;
	public Material damageEffectMat;
	public Material playerMat;
	public static bool damageEffect = false;
	public bool damageRecieved = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void ApplyDamage(float damage)
	{
		if (currentHealth > 0)
		{
			damageRecieved = true;
			currentHealth -= damage;
			if (transform.position.y > 1.531) {
				currentHealth += damage;
				damageRecieved = false;
			}
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Highscore.CheckScore();
				Application.LoadLevel(0);
			}
			else if (damageRecieved)
			{
				StartCoroutine(DamageEffect ());
			}
		}
	}
	
	void RestartScene()
	{
		Scorecounter.ClearScore();
		Application.LoadLevel(0);
	}
	IEnumerator DamageEffect()
	{
		damageEffect = true;
		renderer.material = damageEffectMat;
		yield return new WaitForSeconds(damageEffectTime);
		renderer.material = playerMat;
		yield return new WaitForSeconds(damageEffectTime);
		renderer.material = damageEffectMat;
		yield return new WaitForSeconds(damageEffectTime);
		renderer.material = playerMat;
		yield return new WaitForSeconds(damageEffectTime);
		renderer.material = damageEffectMat;
		yield return new WaitForSeconds(damageEffectTime);
		renderer.material = playerMat;
		yield return new WaitForSeconds(damageEffectTime);
		damageEffect = false;
	}

	void OnGUI()
	{
		GUI.Label(new Rect ((Screen.width - 70),10,70,50),"Blocks Destroyed: "+ Scorecounter.score/Highscore.multipler);
		GUI.Label(new Rect ((Screen.width - 70),60,70,30),"Score: "+ Scorecounter.score);
		GUI.Label(new Rect ((Screen.width - 70),80,70,30),"Difficulty: "+ Highscore.multipler);
		GUI.Label(new Rect ((70),10,70,30),currentHealth+" lifes left");
	}
}
