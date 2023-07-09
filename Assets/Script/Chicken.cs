using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    enum Side 
    {
        Forward,
        Left,
        Right,
    }
    Rigidbody rb;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] LayerMask BarierMask;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float groundCheckDistance = 0.3f;
    [SerializeField] float barierCheckDistance = 2f;
    Collider collider;
    bool PrevIsGrounded = false;
    bool IsGrounded = false;
    //bool ReadyForJump = false;
    int random = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    //IEnumerator WaitForNexJump(float time) 
    //{
    //    yield return new WaitForSeconds(time);
    //    ReadyForJump = true;
    //}
    private void FixedUpdate()
    {
        IsGrounded = CheckIsGround();
        //if (PrevIsGrounded != IsGrounded && IsGrounded) 
        //{
        //    //подумати
        //    StartCoroutine(WaitForNexJump(0.1f));
        //}
        if (IsGrounded) //&& ReadyForJump)
        {
        //    ReadyForJump = false;
            bool canJumpForward = !CheckIsBarier(Side.Forward);
            bool canJumpLeft = !CheckIsBarier(Side.Left);
            bool canJumpRight = !CheckIsBarier(Side.Right);
            if (!canJumpForward && canJumpRight && canJumpLeft)
            {
                Debug.Log("Cant FRWRD");
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    MoveChicken(Side.Right);
                }
                else
                {
                    MoveChicken(Side.Left);
                }
            }
            else if (!canJumpLeft && canJumpForward && canJumpRight)
            {
                Debug.Log("Cant LFT");
                random = Random.Range(0, 101);
                if (random <= 80)
                {
                    MoveChicken(Side.Forward);
                }
                else
                {
                    MoveChicken(Side.Right);
                }
            }
            else if (!canJumpRight && canJumpForward && canJumpLeft)
            {
                Debug.Log("Cant RGHT");
                random = Random.Range(0, 101);
                if (random <= 80)
                {
                    MoveChicken(Side.Forward);
                }
                else
                {
                    MoveChicken(Side.Left);
                }
            }
            else if (!canJumpForward && !canJumpLeft)
            {
                MoveChicken(Side.Right);
            }
            else if (!canJumpForward && !canJumpRight) 
            {
                MoveChicken(Side.Left);
            }
            else
            {
                random = Random.Range(0, 100);
                if (random <= 80)
                {
                    MoveChicken(Side.Forward);
                }
                else if (random > 80 && random <= 90)
                {
                    MoveChicken(Side.Right);
                }
                else if (random > 90 && random <= 100)
                {
                    MoveChicken(Side.Left);
                }
            }
        }
        PrevIsGrounded = IsGrounded;
    }

    void MoveChicken(Side side) 
    {
        switch (side) 
        {
            case Side.Forward:
                PositionAndRotation(new Vector3(0, 180, 0));
                rb.AddForce(new Vector3(0, jumpForce, jumpForce));
                break;
            case Side.Right:
                PositionAndRotation(new Vector3(0, -90, 0));
                rb.AddForce(new Vector3(jumpForce, jumpForce, 0));
                break;
            case Side.Left:
                PositionAndRotation(new Vector3(0, 90, 0));
                rb.AddForce(new Vector3(-jumpForce, jumpForce, 0));
                break;
        }
    }
    bool CheckIsBarier(Side side)
    {
        Vector3 position = transform.position;
        switch (side)
        {
            case Side.Left:
                Vector3 centerL = new Vector3(collider.bounds.center.x - collider.bounds.extents.x, transform.position.y, transform.position.z);
                Debug.DrawLine(centerL, centerL + new Vector3(-2, 0, 0), Color.red, 5);
                return Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, 0.95f), Vector3.left, barierCheckDistance, BarierMask)
                      || Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, -0.95f), Vector3.left, barierCheckDistance, BarierMask)
                      || Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, 0f), Vector3.left, barierCheckDistance, BarierMask);

            case Side.Right:
                Vector3 centerR = new Vector3(collider.bounds.center.x + collider.bounds.extents.x, transform.position.y, transform.position.z);
                Debug.DrawLine(centerR, centerR + new Vector3(2, 0, 0), Color.red, 5);
                return Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, 0.95f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, -0.95f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, 0f), Vector3.right, barierCheckDistance, BarierMask);

            default:
            
                Vector3 centerF = new Vector3(transform.position.x, transform.position.y, collider.bounds.center.z + collider.bounds.extents.z);
                Debug.Log("sdadas");
                Debug.DrawLine(centerF, centerF + new Vector3(0, 0, 2), Color.red,5);
                return Physics.Raycast(centerF + new Vector3(-0.95f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerF + new Vector3(0.95f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerF + new Vector3(0f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask);
        }

    }
   
    bool CheckIsGround()
    {
        Vector3 position = transform.position;
        return Physics.Raycast(position + new Vector3(0f, 0f, 0.9f), Vector3.down, groundCheckDistance, GroundMask)
            || Physics.Raycast(position + new Vector3(0f, 0f, -0.9f), Vector3.down, groundCheckDistance, GroundMask)
            || Physics.Raycast(position + new Vector3(0f, 0f, 0f), Vector3.down, groundCheckDistance, GroundMask);
    }

    void PositionAndRotation(Vector3 newRotation) 
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
        //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }

}
