using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine animationCoroutine;

    [Header("Animation Settings")]
    public float framesPerSecond = 10f;

    [Header("Idle Animation")]
    public Sprite[] idleFrames;

    [Header("Attack Animation Frames")]
    public Sprite[] fireAttackFrames;
    public Sprite[] waterAttackFrames;
    public Sprite[] earthAttackFrames;
    public Sprite[] airAttackFrames;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Start the idle animation initially
        PlayIdle();
    }

    // --- Public Functions for OnClick() Events ---

    public void PlayFireAttack()
    {
        PlayAttack(fireAttackFrames);
    }

    public void PlayWaterAttack()
    {
        PlayAttack(waterAttackFrames);
    }

    public void PlayEarthAttack()
    {
        PlayAttack(earthAttackFrames);
    }

    public void PlayAirAttack()
    {
        PlayAttack(airAttackFrames);
    }

    // --- Animation Control ---

    private void PlayIdle()
    {
        // Only start if there are idle frames
        if (idleFrames == null || idleFrames.Length == 0) return;
        
        StartNewAnimation(Animate(idleFrames, true));
    }

    private void PlayAttack(Sprite[] attackFrames)
    {
        // Only start if there are attack frames
        if (attackFrames == null || attackFrames.Length == 0) return;

        StartNewAnimation(Animate(attackFrames, false));
    }

    private void StartNewAnimation(IEnumerator animationToStart)
    {
        // Stop the current animation if one is running
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        // Start the new one
        animationCoroutine = StartCoroutine(animationToStart);
    }

    private IEnumerator Animate(Sprite[] frames, bool loop)
    {
        do
        {
            foreach (var frame in frames)
            {
                spriteRenderer.sprite = frame;
                yield return new WaitForSeconds(1f / framesPerSecond);
            }
        } while (loop);

        // If the animation was an attack (non-looping), restart the idle animation
        if (!loop)
        {
            PlayIdle();
        }
    }
}
