using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawning : MonoBehaviour
{
    public List<GameObject> objectPrefab;
    private Vector3 spawnPoint;
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 6f;
    private int count;
    void Start()
    { 
        spawnPoint = transform.position;
        SpawnObject();
        count = objectPrefab.Count;
    }
    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnObject();
    }
    void SpawnObject()
    {
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
        Instantiate(objectPrefab[Random.Range(0,count)], spawnPoint, rotation, transform.parent);
        StartCoroutine(Wait());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
