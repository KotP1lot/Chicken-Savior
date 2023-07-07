using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawning : MonoBehaviour
{
    public GameObject objectPrefab;
    private Vector3 spawnPoint;
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 6f;

    void Start()
    { 
        spawnPoint = transform.position;
        Invoke("SpawnObject", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void SpawnObject()
    {
        Instantiate(objectPrefab, spawnPoint, Quaternion.identity);
        Invoke("SpawnObject", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
