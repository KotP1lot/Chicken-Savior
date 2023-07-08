using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera camera;
    [SerializeField]Transform chicken;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, chicken.position.z-17);
    }
}
