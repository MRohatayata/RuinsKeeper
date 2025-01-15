using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Vector3> spawnPositions = new List<Vector3>();
    private List<Vector3> availablePositions;
    private List<GameObject> activeEnemies = new List<GameObject>();
    public int maxEnemies = 1;

    void Start()
    {
        spawnPositions = new List<Vector3>
        {
            new Vector3(8, -8, 1),
            new Vector3(-3, -8, 1),
            new Vector3(0, 0, 1),
            new Vector3(9, -3, 1),
            new Vector3(9, 13, 1),
            new Vector3(-3, 15, 1),
            new Vector3(6, 4, 1),
            new Vector3(15, 1, 1),
            new Vector3(-8, -8, 1)
        };
    }

    public void SpawnEnemies(int level)
    {
        if (level < 1) level = 1; // Minimum kontrol
        if (level <= 6)
        {
            maxEnemies = level;
        }
        else
        {
            maxEnemies = 6;
        }

        Debug.Log("Level: " + level + " için " + maxEnemies + " düþman doðurulacak.");

        availablePositions = new List<Vector3>(spawnPositions);

        for (int i = 0; i < maxEnemies; i++)
        {
            if (availablePositions.Count == 0) break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPosition = availablePositions[randomIndex];

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy); // Aktif düþman listesine ekle
            availablePositions.RemoveAt(randomIndex);
        }
    }

    // Düþmanlar temizlenmiþ mi kontrol et
    public bool AreEnemiesCleared()
    {
        // Listeyi temizle (null olmuþ objeleri çýkar)
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Eðer liste boþsa tüm düþmanlar temizlenmiþtir
        return activeEnemies.Count == 0;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy); // Listedeki düþmaný kaldýr
            Destroy(enemy); // Sahneden yok et
        }
    }
}
