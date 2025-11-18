using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]

public class EvilCatHeadmasterController : MonoBehaviour
{
    [Header("Animation")]
    [Tooltip("The cat opening and closing the book.")]
    public Sprite[] animationFrames;

    [Tooltip("The frame rate of the animation.")]
    public float fps = 7.0f;

    private Rigidbody rb;
    private SpriteRenderer sr;
    private int currentFrame = 0;
    private float animationTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        rb.constraints = RigidbodyConstraints.FreezeRotation
        | RigidbodyConstraints.FreezePositionX
        | RigidbodyConstraints.FreezePositionZ;
    }

    void Start()
    {
        if (animationFrames == null || animationFrames.Length == 0)
        {
            Debug.LogError("EvilCatHeadmasterController: There are no animations for the EvilCatHeadmaster.");
            enabled = false;
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        animationTimer += Time.deltaTime;
        if (animationTimer >= 1f / fps)
        {
            animationTimer = 0f;
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            sr.sprite = animationFrames[currentFrame];
        }

    }
}
