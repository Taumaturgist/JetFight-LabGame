using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerZ : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;

    private float zEnemySpawn = 7.0f;
    private float xSpawnRange = 12.0f;
    private float zPowerupRange = 7.0f;
    private float ySpawn = 2.0f;

    private float startDelayE = 2.0f;
    private float spawnIntervalE = 1.0f;
    private float startDelayP = 5.0f;
    private float spawnIntervalP = 15.0f;

    public GameObject titleScreen;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartSpawn(float difficulty)
    {
        spawnIntervalE *= difficulty;
        titleScreen.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        InvokeRepeating("SpawnEnemy", startDelayE, spawnIntervalE);
        InvokeRepeating("SpawnPowerup", startDelayP, spawnIntervalP);
    }        

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);

    }
    void SpawnPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zPowerupRange, zPowerupRange);
        int randomIndex = Random.Range(0, powerups.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);
        Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
        //Debug.Log("HERE!");
    }
}
