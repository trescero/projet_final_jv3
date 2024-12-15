using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] SpawnTower spawnTower;
 
    int spawnNb = 1;

    public void spawnButton(GameObject tower){ 

        if(spawnNb == 1){
            spawnTower.towerPrefab = tower; 
            spawnNb -= 1;
        }
        else if(spawnNb < 1){

        }
    }
 
}
