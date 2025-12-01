using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableAnimation : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] animationFrames;
    public float frameRate = 0.1f;
    public UnityEvent onClick;

    private Image image;
    private bool isAnimating = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAnimating)
        {
            StartCoroutine(PlayAnimation());
        }
    }

    System.Collections.IEnumerator PlayAnimation()
    {
        isAnimating = true;

        for (int i = 0; i < animationFrames.Length; i++)
        {
            image.sprite = animationFrames[i];
            yield return new WaitForSeconds(frameRate);
        }

        isAnimating = false;
        onClick.Invoke();
    }
}
