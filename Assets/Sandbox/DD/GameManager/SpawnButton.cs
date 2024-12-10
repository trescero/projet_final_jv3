using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] SpawnTower spawnTower;
 
 
    public void spawnButton(GameObject tower){ 
        spawnTower.towerPrefab = tower; 
    }
 
}
