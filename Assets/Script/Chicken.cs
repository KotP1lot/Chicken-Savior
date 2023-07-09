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
    bool PrevIsGrounded = false;
    bool IsGrounded = false;
    //bool ReadyForJump = false;
    int random = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 size = transform.localScale;

        switch (side)
        {
            case Side.Left:
                Debug.DrawRay(position + new Vector3(0f, 0.5f, 0.8f), Vector3.left, Color.red, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(0f, 0.5f, 0f), Vector3.left, Color.red, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(0f, 0.5f, -0.8f), Vector3.left, Color.red, barierCheckDistance);
                
                return Physics.Raycast(position + new Vector3(0f, 0.5f, 0.8f), Vector3.left, barierCheckDistance, BarierMask)
                || Physics.Raycast(position + new Vector3(0f, 0.5f, -0.8f), Vector3.left, barierCheckDistance, BarierMask)
                      || Physics.Raycast(position + new Vector3(0f, 0.5f, 0f), Vector3.left, barierCheckDistance, BarierMask);
    
            case Side.Right:
                Debug.DrawRay(position + new Vector3(0f, 0.5f, 0.8f), Vector3.right, Color.yellow, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(0f, 0.5f, 0f), Vector3.right, Color.yellow, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(0f, 0.5f, -0.8f), Vector3.right, Color.yellow, barierCheckDistance);
                
                return Physics.Raycast(position + new Vector3(0f, 0.5f, 0.8f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(position + new Vector3(0f, 0.5f, -0.8f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(position + new Vector3(0f, 0.5f, 0f), Vector3.right, barierCheckDistance, BarierMask);
   
            default:
                Debug.DrawRay(position + new Vector3(0.8f, 0.5f, 0), Vector3.forward, Color.black, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(0f, 0.5f, 0), Vector3.forward, Color.black, barierCheckDistance);
                Debug.DrawRay(position + new Vector3(-0.8f, 0.5f, 0), Vector3.forward, Color.black, barierCheckDistance);
               
                return Physics.Raycast(position + new Vector3(-0.8f, 0.5f, 0), Vector3.forward, barierCheckDistance, BarierMask)
                    || Physics.Raycast(position + new Vector3(0.8f, 0.5f, 0), Vector3.forward, barierCheckDistance, BarierMask)
               || Physics.Raycast(position + new Vector3(0f, 0.5f, 0f), Vector3.forward, barierCheckDistance, BarierMask);
     

        }
    
    }
   
    bool CheckIsGround()
    {
        return Physics.Raycast((transform.position + Vector3.up * 0.1f), Vector3.down, groundCheckDistance, GroundMask);
    }

    void PositionAndRotation(Vector3 newRotation) 
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
        //transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }

}
