using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	//public float startSpeed = 10f;
	//[HideInInspector]
	//public float speed;

	//le montant de point que coute l'ennemi lorsque tuer
	/*public int worth = 50;*/
    

	[Header("Vie")]
	public EnnemiValeurs ScriptEnnemiValeurs;
	public GameObject deathEffect;
	
	[Header("Tower")]
	public string towerTag = "Tower";
	public bool isTower;

	[Header("HealthBar")]
	public Healthbar HealthbarScript;
	NavMeshAgent agent;
	private bool isDead = false;
	[HideInInspector] public float health;
	[HideInInspector] public float damage;

	void Start ()
	{
		//speed = startSpeed;
		health = ScriptEnnemiValeurs.VieDepart;
		damage = ScriptEnnemiValeurs.DamageDepart;
		Debug.Log(ScriptEnnemiValeurs.DamageDepart);


		if(!isTower)
		{			
        	agent = GetComponent<NavMeshAgent>();
			agent.speed = ScriptEnnemiValeurs.SpeedDepart;
			agent.SetDestination(GameObject.FindGameObjectWithTag("Tower").transform.position);
		}
	}

	public void TakeDamage (float amount)
	{
		health -= amount;
		HealthbarScript._healthbarSprite.fillAmount = health / ScriptEnnemiValeurs.VieDepart;
		HealthbarScript.healthText.text = $"{Mathf.CeilToInt(health)} HP";

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}

	/*public void Slow (float pct)
	{
		speed = startSpeed * (1f - pct);
	}*/

	void Die ()
	{
		isDead = true;

		//PlayerStats.Money += worth;
		Debug.Log("ARGENT :" + ScriptEnnemiValeurs.ArgentDepart);
		Debug.Log("point :" + ScriptEnnemiValeurs.PointDepart);

		//WaveSpawner.EnemiesAlive--;
		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 2f);

		Destroy(gameObject);
	}
}