using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Valeurs/Player", order = 1)]

public class Player_ScriptableObject : ScriptableObject
{
	public int money;
    public int points;
    public bool towerInHand;
    public bool hasPlacedFirstTower;

}