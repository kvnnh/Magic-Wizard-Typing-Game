using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
        
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 10.0f;
    public float spawnRange = 10.0f;

    public Player player;


  

    private void Start()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is not set or empty. Please assign prefabs in the inspector.");
            return;
        }

        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (player.isDead == false)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is empty.");
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyObject = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity, transform);

        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (playerScript != null)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            playerScript.RegisterEnemy(enemy);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float screenAspect = Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * screenAspect;

        int direction = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (direction)
        {
            case 0:
                spawnPosition = new Vector3(Random.Range(-screenWidth / 2, screenWidth / 2), Camera.main.orthographicSize, 0);
                break;
            case 1:
                spawnPosition = new Vector3(Random.Range(-screenWidth / 2, screenWidth / 2), -Camera.main.orthographicSize, 0);
                break;
            case 2:
                spawnPosition = new Vector3(-screenWidth / 2, Random.Range(-screenHeight / 2, screenHeight / 2), 0);
                break;
            case 3:
                spawnPosition = new Vector3(screenWidth / 2, Random.Range(-screenHeight / 2, screenHeight / 2), 0);
                break;
        }

        return spawnPosition;
    }

}
