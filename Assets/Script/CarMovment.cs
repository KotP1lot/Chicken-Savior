using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float minSpeed, maxSpeed, cursorSpeed;
    public LayerMask groundLayer;
    private Rigidbody rb;
    private Vector3 velocity;
    private bool isMoving = true;
    private float tmpmin,tmpmax;
    private Vector3 startPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(minSpeed, maxSpeed),0, 0);
    }
    void ChangeMass(float newMass)
    {
        rb.mass = newMass;
    }
    void Update()
    {
        if (isMoving && IsOnGround())
        {
            rb.velocity = velocity;
        }
        else if (!IsOnGround())
        {
            Destroy(transform.parent.gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {                    
                    isMoving = !isMoving;
                    rb.velocity -= velocity;
                }
            }
        }

        if (Input.GetMouseButton(0) && !isMoving)
        {
            float mouseX = Input.GetAxis("Mouse X");

            if (mouseX < 0)
            {
                // –ух миш≥ вл≥во
                transform.position -= new Vector3(cursorSpeed, 0, 0);
            }
            else if (mouseX > 0)
            {
                // –ух миш≥ вправо
                transform.position += new Vector3(cursorSpeed, 0, 0);
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            isMoving = true;
         
        }
    }

    private bool IsOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit,12f, groundLayer))
        {
            return true;
        }
        return false;
    }
}