using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    

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

	[SerializeField] private Player_ScriptableObject _player;

	void Start ()
	{
		health = ScriptEnnemiValeurs.VieDepart;
		damage = ScriptEnnemiValeurs.DamageDepart;


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


	void Die ()
	{
		isDead = true;


		_player.money += ScriptEnnemiValeurs.ArgentDepart;
		_player.points += ScriptEnnemiValeurs.PointDepart;

		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 2f);

		Destroy(gameObject);
	}
}