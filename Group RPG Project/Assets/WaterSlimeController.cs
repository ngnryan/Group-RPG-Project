using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]

public class WaterSlimeController : MonoBehaviour
{
    [Header("Animation")]
    [Tooltip("The sprites for the bounce animation")]
    public Sprite[] animationFrames; //intializing the array in which the animations frames are

    [Tooltip("The frame rate of the animation.")]
    public float fps = 7.0f;

    [Header("Movement")]
    [Tooltip("The upward force of the slime's jump.")]
    public float jumpForce = 5.0f;

    [Tooltip("The time in seconds between each jump.")]
    public float jumpInterval = 2.5f;

    private Rigidbody rb;
    private SpriteRenderer sr;
    private int currentFrame = 0;
    private float animationTimer = 0f;
    private float jumpTimer = 0f;

    void Awake() //always, not just on press-good convention
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        rb.constraints = RigidbodyConstraints.FreezeRotation
        | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; //lock in place
    }

    void Start()
    {
        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogError("WaterSlimeController: There are no animation frames");
            enabled = false;
            return;
        }

    }

    // Update is called once per frame
    void Update() //animation (update per frame)
    {
        animationTimer += Time.deltaTime; //keeps it consistent across all interfaces--not device dependent
        if (animationTimer >= 1f / fps) //when the timer starts (dictates the S in FPS)
        {
            animationTimer = 0f; //resets timer?
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            sr.sprite = animationFrames[currentFrame];
        }

        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpInterval)
        {
            jumpTimer = 0f;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
