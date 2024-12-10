using UnityEngine;

public class Bullet : MonoBehaviour {

	public Tourelles_Parametres projectileValues;
	private Transform target;
	
	public void Seek (Transform _target)
	{
		target = _target;
	}

	// Update is called once per frame
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = projectileValues.speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(projectileValues.impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (projectileValues.explosionRadius > 0f)
		{
			Explode();
		} else
		{
			Damage(target);
		}

		Destroy(gameObject);
		Debug.Log("hit");
	}

	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, projectileValues.explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null)
		{
			e.TakeDamage(projectileValues.damage);
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, projectileValues.explosionRadius);
	}
}