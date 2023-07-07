using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float groundCheckDistance = 0.3f;
    [SerializeField] bool isGrounded = false;


}
