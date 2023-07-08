using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> plane;
    [SerializeField] List<GameObject> AllPlanes;
    int NextSpawnPosZ = 12;
    void Start()
    {
        SpawnNewPlane();
    }
    public void SpawnNewPlane() 
    {
        GameObject newPlane = Instantiate(plane[Random.Range(0,plane.Count)], transform);
        newPlane.transform.position = new Vector3(0, 0, NextSpawnPosZ);
        AllPlanes.Add(newPlane);
        if (AllPlanes.Count > 8) 
        {
            Destroy(AllPlanes[0]);
            AllPlanes.RemoveAt(0);
        }
        NextSpawnPosZ += 6;
    }
    public void Restart() 
    {
        AllPlanes.Clear();
        NextSpawnPosZ = 12;
        SpawnNewPlane();
    }
}
