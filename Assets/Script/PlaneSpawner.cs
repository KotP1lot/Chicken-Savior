using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> plane;
    [SerializeField] List<GameObject> empty;
    [SerializeField] List<GameObject> water;
    [SerializeField] List<GameObject> AllPlanes;
    int NextSpawnPosZ = 12;
    void Start()
    {
        SpawnNewPlane(true);
        SpawnNewPlane(true);
    }
    public void SpawnNewPlane(bool isStart) 
    {
        if (isStart) SetPlane(empty);
        else
        {
            int random = Random.Range(0, 101);
            if (random <= 60)
            {
                SetPlane(plane);
            }
            else if (random > 60 && random <= 80)
            {
                SetPlane(water);
            }
            else 
            {
                SetPlane(empty);
            }
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
}
