using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] private SpawnTower spawnTower;

    // This method will replace the towerPrefab in SpawnTower when the button is pressed
    public void SpawnButtonAction(GameObject newTowerPrefab)
    {
        spawnTower.SetTowerPrefab(newTowerPrefab);
        
    }
}
