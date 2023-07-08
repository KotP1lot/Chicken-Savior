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
    [SerializeField] float barierCheckDistance = 0.3f;
    bool PrevIsGrounded = false;
    bool IsGrounded = false;
    bool ReadyForJump = false;
    int random = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    IEnumerator WaitForNexJump(float time) 
    {
        yield return new WaitForSeconds(time);
        ReadyForJump = true;
    }
    private void Update()
    {
        IsGrounded = CheckIsGround();
        if (PrevIsGrounded != IsGrounded && IsGrounded) 
        {
            //подумати
            StartCoroutine(WaitForNexJump(1f));
        }
        if (IsGrounded && ReadyForJump)
        {
            ReadyForJump = false;
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
                if (random <= 70)
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
                if (random <= 70)
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
                if (random <= 70)
                {
                    MoveChicken(Side.Forward);
                }
                else if (random > 70 && random <= 85)
                {
                    MoveChicken(Side.Right);
                }
                else if (random > 85 && random <= 100)
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
        switch (side)
        {
            case Side.Left:
                Debug.DrawRay(transform.position, Vector3.left, Color.red, barierCheckDistance);
                return Physics.Raycast(transform.position, Vector3.left, barierCheckDistance, BarierMask);
                break; 
            case Side.Right:
                Debug.DrawRay(transform.position, Vector3.right, Color.green, barierCheckDistance);
                return Physics.Raycast(transform.position, Vector3.right, barierCheckDistance, BarierMask);
                break; 
            default:
                Debug.DrawRay(transform.position, Vector3.forward, Color.yellow, barierCheckDistance);
                return Physics.Raycast(transform.position, Vector3.forward, barierCheckDistance, BarierMask);
                break;
        
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
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }
}
