using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawning : MonoBehaviour
{
    EventSyst syst;
    public List<GameObject> objectPrefab;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private Vector3 spawnPoint;
    private float minSpawnDelay = 3f;
    private float maxSpawnDelay = 6f;
    bool isStart = true;
    private int count;
    void Start()
    { 
        spawnPoint = transform.position;
        count = objectPrefab.Count;
        SpawnObject();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(isStart?0:Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnObject();
    }
    void SpawnObject()
    {
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
        if(spawnedObjects.Count <= 3) spawnedObjects.Add(Instantiate(objectPrefab[Random.Range(0, count)], spawnPoint, rotation, transform.parent));
        StartCoroutine(Wait());
        isStart = false;
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    void OnCarDestroyed() 
    {
        foreach(GameObject obj in spawnedObjects) 
        {
            if (obj == null) spawnedObjects.Remove(obj);
            return;
        }
    }
    private void OnEnable()
    {
        syst = GameObject.Find("EventSys").GetComponent<EventSyst>();

        syst.OnCarDestroyed += OnCarDestroyed;
    }
}
