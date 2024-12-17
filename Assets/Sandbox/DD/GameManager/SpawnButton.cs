using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] private SpawnTower spawnTower;

    // Remplace le prefab tower dans le script de spawn
    public void SpawnButtonAction(GameObject newTowerPrefab)
    {
        spawnTower.SetTowerPrefab(newTowerPrefab);
    }

    public void setSoclesAtives(bool state)
    {
        spawnTower.ToggleSocles(state);
    }
}
