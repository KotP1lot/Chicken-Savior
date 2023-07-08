using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> plane;
    [SerializeField] List<GameObject> empty;
    [SerializeField] List<GameObject> AllPlanes;
    int NextSpawnPosZ = 12;
    void Start()
    {
        SpawnNewPlane();
        SpawnNewPlane();
    }
    public void SpawnNewPlane() 
    {
        int random = Random.Range(0, 101);
        if (random <= 70)
        {
            SetPlane(plane);
        }
        else 
        {
            SetPlane(empty);
        }
 
        NextSpawnPosZ += 6;
    }
    private void SetPlane(List<GameObject> planes) 
    {
        GameObject newPlane = Instantiate(planes[Random.Range(0, planes.Count)], transform);
        newPlane.transform.position = new Vector3(0, 0, NextSpawnPosZ);
        AllPlanes.Add(newPlane);
        if (AllPlanes.Count > 8)
        {
            Destroy(AllPlanes[0]);
            AllPlanes.RemoveAt(0);
        }
    }
    public void Restart() 
    {
        AllPlanes.Clear();
        NextSpawnPosZ = 12;
        SpawnNewPlane();
    }
}
