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


    [Header("Walking Sprites")]
    public Sprite[] walkUpSprites;
    public Sprite[] walkDownSprites;
    public Sprite[] walkRightSprites;
    public Sprite[] walkLeftSprites;

    public float walkFrameRate = 8f;

    [Header("Audio")]
    [Tooltip("The sound effect for the character's footsteps.")]
    public AudioClip walkingSound;

    [Header("Lost Sprites")]
    public Sprite lostBackSprite;
    public Sprite lostFrontSprite;
    public Sprite lostLeftSprite;
    public Sprite lostRightSprite;
    public Sprite[] lostWalkUpSprites;
    public Sprite[] lostWalkDownSprites;
    public Sprite[] lostWalkRightSprites;
    public Sprite[] lostWalkLeftSprites;

    private Animator animator;

    private const float deadZone = 0.1f;
    private Rigidbody rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private Vector3 moveInput;

    private float walkTimer = 0f;
    private int walkFrameIndex = 0;

    private Vector3 lastMoveDirection = Vector3.forward;

    private void CheckForLostState()
    {
        if (PlayerPrefs.GetInt("BattleLost", 0) == 1)
        {
            // Swap to lost sprites if they are assigned
            if (lostBackSprite != null) backSprite = lostBackSprite;
            if (lostFrontSprite != null) frontSprite = lostFrontSprite;
            if (lostLeftSprite != null) leftSprite = lostLeftSprite;
            if (lostRightSprite != null) rightSprite = lostRightSprite;

            if (lostWalkUpSprites != null && lostWalkUpSprites.Length > 0) walkUpSprites = lostWalkUpSprites;
            if (lostWalkDownSprites != null && lostWalkDownSprites.Length > 0) walkDownSprites = lostWalkDownSprites;
            if (lostWalkRightSprites != null && lostWalkRightSprites.Length > 0) walkRightSprites = lostWalkRightSprites;
            if (lostWalkLeftSprites != null && lostWalkLeftSprites.Length > 0) walkLeftSprites = lostWalkLeftSprites;
        }
    }

    private Sprite[] GetWalkSprites()
    {
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.z))
        {
            //Left-Right
            if (moveInput.x > 0)
            {
                return walkRightSprites;
            }
            else
            {
                return walkLeftSprites;
            }
        }
        else
        {
            //Up-Down
            if (moveInput.z > 0)
            {
                return walkUpSprites;
            }
            else
            {
                return walkDownSprites;
            }
        }
    }

    private void UpdateWalkingAnimation()
{
    Sprite[] currentWalkSet = null;

    // Pick the correct animation set based on direction:
    if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.z))
    {
        // Horizontal movement
        currentWalkSet = moveInput.x > 0 ? walkRightSprites : walkLeftSprites;
    }
    else
    {
        // Vertical movement
        currentWalkSet = moveInput.z > 0 ? walkUpSprites : walkDownSprites;
    }

    if (currentWalkSet == null || currentWalkSet.Length == 0)
        return; // No animation frames assigned

    // Update animation frame timing
    walkTimer += Time.deltaTime;

    if (walkTimer >= 1f / walkFrameRate)
    {
        walkTimer = 0f;
        walkFrameIndex++;

        if (walkFrameIndex >= currentWalkSet.Length)
            walkFrameIndex = 0;

        sr.sprite = currentWalkSet[walkFrameIndex];
    }
}




    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (sr == null)
        {
            Debug.LogError("PlayerController could NOT find a SpriteRenderer in any child objects. Sprite switching will not work.");
        }

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        CheckForLostState();
    }

    void Update()
    {
        // Store input values in Update for use in other methods
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector3(horizontalInput, 0, verticalInput);

        if (moveInput.magnitude > deadZone)
        {
            lastMoveDirection = moveInput;
        }

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
            /** --- Sprite switching logic ---
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
            **/
            lastMoveDirection = moveInput;

            // --- Audio logic ---
            if (walkingSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
            UpdateWalkingAnimation();
        }
        else
        {
            // If not moving, stop the sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.z))
            {
                sr.sprite = lastMoveDirection.x > 0 ? rightSprite : leftSprite;
            }
            else
            {
                sr.sprite = lastMoveDirection.z > 0 ? backSprite : frontSprite;
            }

            walkFrameIndex = 0;
            walkTimer = 0f;
        }
    }
}
