using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The movement speed of the character.")]
    public float speed = 5.0f;

    [Header("Directional Sprites")]
    [Tooltip("Sprite to display when moving up.")]
    public Sprite backSprite;

    [Tooltip("Sprite to display when moving down.")]
    public Sprite frontSprite;

    [Tooltip("Sprite to display when moving left.")]
    public Sprite leftSprite;

    [Tooltip("Sprite to display when moving right.")]
    public Sprite rightSprite;

    [Header("Audio")]
    [Tooltip("The sound effect for the character's footsteps.")]
    public AudioClip walkingSound;

    private Animator animator;

    private const float deadZone = 0.1f;
    private Rigidbody rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private Vector3 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (sr == null)
        {
            Debug.LogError("PlayerController could NOT find a SpriteRenderer in any child objects. Sprite switching will not work.");
        }

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Store input values in Update for use in other methods
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector3(horizontalInput, 0, verticalInput);

        //Animation
        bool isMoving = moveInput.magnitude > deadZone;

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", horizontalInput);
        animator.SetFloat("MoveZ", verticalInput);

    }

    void FixedUpdate()
    {
        // Apply physics-based movement in FixedUpdate
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        bool isMoving = moveInput.magnitude > deadZone;

        // Handle visual and audio updates in LateUpdate
        if (isMoving)
        {
            // --- Sprite switching logic ---
            if (sr != null)
            {
                if (Mathf.Abs(moveInput.x) > deadZone)
                {
                    if (moveInput.x > 0) sr.sprite = rightSprite;
                    else sr.sprite = leftSprite;
                }
                else if (Mathf.Abs(moveInput.z) > deadZone)
                {
                    if (moveInput.z > 0) sr.sprite = backSprite;
                    else sr.sprite = frontSprite;
                }
            }

            // --- Audio logic ---
            if (walkingSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
        }
        else
        {
            // If not moving, stop the sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
