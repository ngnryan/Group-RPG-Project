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
    public Sprite[] lostIdleFrames;

    [Header("Attack Animation")]
    public Sprite[] attackFrames;
    public Sprite[] lostAttackFrames;

    private Sprite[] currentIdleFrames;
    private Sprite[] currentAttackFrames;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Default to normal frames
        currentIdleFrames = idleFrames;
        currentAttackFrames = attackFrames;

        // Check for Lost State
        if (PlayerPrefs.GetInt("BattleLost", 0) == 1)
        {
            if (lostIdleFrames != null && lostIdleFrames.Length > 0) currentIdleFrames = lostIdleFrames;
            if (lostAttackFrames != null && lostAttackFrames.Length > 0) currentAttackFrames = lostAttackFrames;
        }

        // Start the idle animation initially
        PlayIdle();
    }

    // --- Public Functions for OnClick() Events ---

    public void PlayFireAttack()
    {
        PlayAttack(currentAttackFrames);
    }

    public void PlayWaterAttack()
    {
        PlayAttack(currentAttackFrames);
    }

    public void PlayEarthAttack()
    {
        PlayAttack(currentAttackFrames);
    }

    public void PlayAirAttack()
    {
        PlayAttack(currentAttackFrames);
    }

    // --- Animation Control ---

    private void PlayIdle()
    {
        // Only start if there are idle frames
        if (currentIdleFrames == null || currentIdleFrames.Length == 0) return;
        
        StartNewAnimation(Animate(currentIdleFrames, true));
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
