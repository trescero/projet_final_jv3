using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> spawnPositions;
    public KeyCode keyToPress = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)].transform.position;
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
