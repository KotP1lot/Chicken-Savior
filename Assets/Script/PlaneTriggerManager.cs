using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTriggerManager : MonoBehaviour
{
    PlaneSpawner planeSpawner;
    bool isAlreadySpawnl= false;
    private void Start()
    {
        planeSpawner = GameObject.Find("Planes").GetComponent<PlaneSpawner>();
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player") 
        {
            if (!isAlreadySpawnl) 
            {
                planeSpawner.SpawnNewPlane(false);
                isAlreadySpawnl = true;
            }
        }
    }
}
