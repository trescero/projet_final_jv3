using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds spawn positions, spawns a specific number of prefabs, and removes used positions.
/// </summary>
public class DynamicSpawnHandler : MonoBehaviour
{
    [Header("Prefabs a spawn")]
    [Tooltip("Prefab Socle")]
    [SerializeField] private GameObject prefabSocle;

    [Tooltip("Prefab tour")]
    [SerializeField] private GameObject prefabTour;

    [Header("Spawn Settings")]
    [Tooltip("Trouver tag dans scene")]
    [SerializeField] private string spawnTag = "SpawnPoint";

    private List<Transform> spawnPoints = new();

    private void Start()
    {
        // Trouve tout les spawn points
        FindSpawnPoints();

        SpawnPrefabs();
    }

    private void FindSpawnPoints()
    {
        spawnPoints.Clear();
        GameObject[] points = GameObject.FindGameObjectsWithTag(spawnTag);

        foreach (GameObject point in points)
        {
            spawnPoints.Add(point.transform);
        }

        Debug.Log($"Found {spawnPoints.Count} spawn points with tag '{spawnTag}'.");
    }

    public void SpawnPrefabs()
    {
        if (spawnPoints.Count < 9)
        {
            Debug.LogWarning("Not enough spawn points for 9 prefabs!");
            return;
        }

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // spawn les 8 socles
        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = GetRandomSpawnPoint(availableSpawnPoints);
            Instantiate(prefabSocle, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Spawned Main Prefab at {spawnPoint.position}");
        }

        // spawn la tour
        Transform uniqueSpawnPoint = GetRandomSpawnPoint(availableSpawnPoints);
        Instantiate(prefabTour, uniqueSpawnPoint.position, uniqueSpawnPoint.rotation);
        Debug.Log($"Spawned Unique Prefab at {uniqueSpawnPoint.position}");
    }


    private Transform GetRandomSpawnPoint(List<Transform> availablePoints)
    {
        int index = Random.Range(0, availablePoints.Count);
        Transform selectedPoint = availablePoints[index];
        availablePoints.RemoveAt(index);
        return selectedPoint;
    }
}
