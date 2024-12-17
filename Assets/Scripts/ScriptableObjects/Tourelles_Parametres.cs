using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Valeurs/Tourelles", order = 1)]
public class Tourelles_Parametres : ScriptableObject
{
    [Header("Tourelle")]
	public float range = 15f;
    public GameObject prefabProjectile;
    public float fireRate = 1f;
	public float turnSpeed = 10f;

    public int cost = 300;
    
    [Header("Projectile")]
    public float speed = 70f;

	public int damage = 50;

	public float explosionRadius = 0f;
	public GameObject impactEffect;

}