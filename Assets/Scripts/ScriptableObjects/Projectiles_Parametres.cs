using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Valeurs/Projectiles", order = 1)]
public class Projectiles_Parametres : ScriptableObject
{
	public float speed = 70f;
	public int damage = 50;
	public float explosionRadius = 0f;
	public GameObject impactEffect;
}