using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The movement speed of the character.")]
    public float speed = 5.0f;

    [Tooltip("Check this box if your sprite faces left by default.")]
    public bool invertFlipLogic = false;

    private const float deadZone = 0.1f;
    private Rigidbody rb;
    private SpriteRenderer sr;
    private Vector3 moveInput;
    private float horizontalInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>(); 

        if (sr == null)
        {
            Debug.LogError("PlayerController could NOT find a SpriteRenderer in any child objects. Flipping will not work.");
        }

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Store input values in Update for use in other methods
        horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Apply physics-based movement in FixedUpdate
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        // Handle visual updates in LateUpdate to override any animation changes.
        if (sr != null)
        {
            if (horizontalInput < -deadZone)
            {
                // Moving left
                sr.flipX = !invertFlipLogic;
            }
            else if (horizontalInput > deadZone)
            {
                // Moving right
                sr.flipX = invertFlipLogic;
            }
        }
    }
}