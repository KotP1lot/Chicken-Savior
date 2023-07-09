using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    private float cursorSpeed = 0.1f;
    public LayerMask groundLayer;
    public LayerMask CarMask;
    [SerializeField] bool IsLeft;
    private Rigidbody rb;
    private Vector3 velocity;
    private bool isMoving = true;
    private bool isMouseDown = false;
    Collider col;
    void Start()
    {
        col = GetComponent<Collider>();
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
        if (isCarForward() && isMoving && !isMouseDown)
        {
            isMoving = false;
            rb.velocity -= velocity;
        }
       
        else if (!isCarForward() && !isMouseDown) 
        {
            isMoving = true;
        }
    }
    private void OnMouseDown()
    {
        isMoving = false;
        isMouseDown = true;
        rb.velocity -= velocity;
    }
    private void OnMouseDrag()
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
    private void OnMouseUp()
    {
        isMoving = true;
        isMouseDown = false;
    }
    private bool isCarForward() 
    {
        if (IsLeft) 
        {
            Vector3 centerL = new Vector3(col.bounds.center.x - col.bounds.extents.x, transform.position.y, transform.position.z);
            return Physics.Raycast(centerL, Vector3.left, 3, CarMask); 
        }
        Vector3 centerR = new Vector3(col.bounds.center.x + col.bounds.extents.x, transform.position.y, transform.position.z);
        return Physics.Raycast(centerR, Vector3.right, 3, CarMask);
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