using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject towerEnterEffect;
    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Ennemi"))
        {
            GameObject effectIns = (GameObject)Instantiate(towerEnterEffect, other.transform.position, Quaternion.identity);
		    Destroy(effectIns, 2f);

            Destroy(other.gameObject);

            Enemy script = other.gameObject.GetComponent<Enemy>();
            
            Enemy e = gameObject.GetComponent<Enemy>();
            e.TakeDamage(script.damage);
            Debug.Log("Enleve des points / scores");
            Debug.Log(script.damage);
        }
		
	}
}
