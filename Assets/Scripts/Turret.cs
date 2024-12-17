using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public Tourelles_Parametres tourrellesValues;

	private Transform target;
	private Enemy targetEnemy;
    public AudioSource fireSound;

	[Header("General")]
	public GameObject batterie;
	
	[Header("Use Bullets (default)")]
	private float fireCountdown = 0f;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public Transform firePoint;
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= tourrellesValues.range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();
		} else
		{
			target = null;
		}
	}
	void Update () {
		if (target == null)
		{
			return;
		}
		LockOnTarget();		
		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / tourrellesValues.fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * tourrellesValues.turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(tourrellesValues.prefabProjectile, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		fireSound.Play();
		//Debug.Log("shoot");

		if (bullet != null)
			bullet.Seek(target);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, tourrellesValues.range);
	}

	public void Placer()
	{
		batterie.SetActive(true);
	}
}