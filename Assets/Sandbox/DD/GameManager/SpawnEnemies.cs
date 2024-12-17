using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject PrefabWeakGround;
    public GameObject PrefabStrongGround;
    public GameObject PrefabAir;

    [Header("References")]
    public FindSpawnPositions spawnPositionsScript;
    public Transform mainTower;

    [Header("Spawning Settings")]
    public int initialNbGroundEnemies = 5; // nb initial ennemis sol
    public int initialNbStrongGroundEnemies = 2; //nb initial ennemis forts
    public int initialNbAirEnemies = 2; //nombre initial d'ennemis volants
    public float initialSpawnInterval = 2f; // intervale de spawn de base
    public float airEnemySpawnRadius = 10f; // radius de spawn ennmis

    [Header("Wave Settings")]
    public int totalWaves = 5; // Waves totales
    public float spawnIntervalReduction = 0.2f; // intervalles de spawn entre les ennemis
    public int enemiesIncrementPerWave = 3; // Augmentation d'ennemis par wave
    public int strongerEnemiesIncrementPerWave = 1; // Augmentation d'ennemis forts par wave

    private int currentWave = 1;
    private bool spawningWave = false;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        while (currentWave <= totalWaves)
        {
            spawningWave = true;

            int groundEnemiesToSpawn = initialNbGroundEnemies + (currentWave - 1) * enemiesIncrementPerWave;
            int strongGroundEnemiesToSpawn = initialNbStrongGroundEnemies + (currentWave - 1) * strongerEnemiesIncrementPerWave;
            int airEnemiesToSpawn = initialNbAirEnemies + (currentWave - 1);
            float spawnInterval = Mathf.Max(0.5f, initialSpawnInterval - (currentWave - 1) * spawnIntervalReduction);

            // Spawn ennemis reguliers
            for (int i = 0; i < groundEnemiesToSpawn; i++)
            {
                SpawnGroundEnemy(PrefabWeakGround); //
                yield return new WaitForSeconds(spawnInterval);
            }

            // Spawn ennemis sol forts
            for (int i = 0; i < strongGroundEnemiesToSpawn; i++)
            {
                SpawnGroundEnemy(PrefabStrongGround);
                yield return new WaitForSeconds(spawnInterval);
            }

            // Spawn ennemis volants
            for (int i = 0; i < airEnemiesToSpawn; i++)
            {
                SpawnAirEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            spawningWave = false;

            // Attend que joueur tue la wave
            yield return new WaitUntil(() => AreAllEnemiesDefeated());

            currentWave++;
        }

        Debug.Log("All waves completed!");
    }

    private void SpawnGroundEnemy(GameObject enemyPrefab)
    {
        if (spawnPositionsScript.EnemySpawnLocations.Count == 0)
        {
            Debug.LogWarning("No spawn locations available!");
            return;
        }

        // Selection de spawn point random
        var randomLocation = spawnPositionsScript.EnemySpawnLocations[Random.Range(0, spawnPositionsScript.EnemySpawnLocations.Count)];
        Instantiate(enemyPrefab, randomLocation.position, randomLocation.rotation);
    }

    private void SpawnAirEnemy()
    {
        // Position random dans le radius autour de la tour
        Vector3 randomOffset = Random.insideUnitSphere * airEnemySpawnRadius;
        randomOffset.y = Mathf.Abs(randomOffset.y); // Assure que ennemis volants spawnent pas sur le sol
        Vector3 spawnPosition = mainTower.position + randomOffset;

        Instantiate(PrefabAir, spawnPosition, Quaternion.identity);
    }

    private bool AreAllEnemiesDefeated()
    {
        // Regarde que aucun ennemi soit en vie
        return GameObject.FindGameObjectsWithTag("Ennemi").Length == 0;
    }
}
