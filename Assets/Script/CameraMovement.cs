using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera cam;
    [SerializeField]Transform chicken;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, chicken.position.z-16);
    }
}
