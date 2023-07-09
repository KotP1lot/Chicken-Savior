using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    [SerializeField] Animator animator;
    [SerializeField] LayerMask BarierMask;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float groundCheckDistance = 0.3f;
    [SerializeField] float barierCheckDistance = 2f;
    Collider col;
    AudioSource audioSource;
    [SerializeField]AudioClip clip;
    Vector3 pos;
    EventSyst syst;
    bool IsGrounded = false;
    bool IsDead = false;
    bool isDeadByWater = false;

    int random = 0;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if (!IsDead)
        {
            IsGrounded = CheckIsGround();

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
                else if (!canJumpForward && !canJumpLeft && canJumpRight)
                {
                    MoveChicken(Side.Right);
                }
                else if (!canJumpForward && !canJumpRight && canJumpLeft)
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
        }
        if(isDeadByWater) 
        {
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }
    }

    void MoveChicken(Side side) 
    {
        animator.SetTrigger("Move");
        switch (side) 
        {
            case Side.Forward:
                syst.OnStepForward?.Invoke();
                PositionAndRotation(new Vector3(0, 180, 0));
                rb.AddForce(new Vector3(0, jumpForce+0.5f, jumpForce));
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
                Vector3 centerL = new Vector3(col.bounds.center.x - col.bounds.extents.x, transform.position.y, transform.position.z);
                Debug.DrawLine(centerL, centerL + new Vector3(-2, 0, 0), Color.red, 5);
                return Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, 1f), Vector3.left, barierCheckDistance, BarierMask)
                      || Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, -1f), Vector3.left, barierCheckDistance, BarierMask)
                      || Physics.Raycast(centerL + new Vector3(-0.1f, 0.5f, 0f), Vector3.left, barierCheckDistance, BarierMask);

            case Side.Right:
                Vector3 centerR = new Vector3(col.bounds.center.x + col.bounds.extents.x, transform.position.y, transform.position.z);
                Debug.DrawLine(centerR, centerR + new Vector3(2, 0, 0), Color.red, 5);
                return Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, 1f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, -1f), Vector3.right, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerR + new Vector3(0.1f, 0.5f, 0f), Vector3.right, barierCheckDistance, BarierMask);

            default:
            
                Vector3 centerF = new Vector3(transform.position.x, transform.position.y, col.bounds.center.z + col.bounds.extents.z);
                Debug.DrawLine(centerF, centerF + new Vector3(0, 0, 2), Color.red,5);
                return Physics.Raycast(centerF + new Vector3(-1f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerF + new Vector3(1f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask)
                    || Physics.Raycast(centerF + new Vector3(0f, 0.5f, 0.1f), Vector3.forward, barierCheckDistance, BarierMask);
        }

    }
   
    bool CheckIsGround()
    {
        Vector3 position = transform.position;
        return Physics.Raycast(position + new Vector3(0f, 0f, 0.95f), Vector3.down, groundCheckDistance, GroundMask)
            || Physics.Raycast(position + new Vector3(0f, 0f, -0.95f), Vector3.down, groundCheckDistance, GroundMask)
            || Physics.Raycast(position + new Vector3(0f, 0f, 0f), Vector3.down, groundCheckDistance, GroundMask);
    }

    void PositionAndRotation(Vector3 newRotation) 
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
    }
    void OnDead(TypeDie typeDie) 
    {
        audioSource.PlayOneShot(clip);
        switch (typeDie)
        {
            case TypeDie.Car:
                animator.Play("Dead");
                rb.isKinematic = true;
                col.enabled = false;
                IsDead = true;
                transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
                break;
            case TypeDie.Water:
                IsDead = true;
                col.isTrigger = true;
                pos = transform.position;
                isDeadByWater = true;
                break;
        }

    }
    private void OnEnable()
    {
        syst = GameObject.Find("EventSys").GetComponent<EventSyst>();

        syst.OnDead += OnDead;
    }
    private void OnDisable()
    {
        syst.OnDead -= OnDead;
    }

}
