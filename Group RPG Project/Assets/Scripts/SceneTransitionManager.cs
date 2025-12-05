using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Tooltip("The canvas group used for fading.")]
    public CanvasGroup fadeCanvasGroup;

    [Tooltip("The duration of the fade.")]
    public float fadeDuration = 1.0f;

    [Tooltip("The color of the fade.")]
    public Color fadeColor = Color.black;

    private Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupUI()
    {
        if (fadeCanvasGroup == null)
        {
            // Create Canvas
            GameObject canvasObj = new GameObject("TransitionCanvas");
            canvasObj.transform.SetParent(transform);
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999; // Ensure it's on top

            // Create CanvasScaler
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();

            // Create Image
            GameObject imageObj = new GameObject("FadeImage");
            imageObj.transform.SetParent(canvasObj.transform, false);
            fadeImage = imageObj.AddComponent<Image>();
            fadeImage.color = fadeColor;
            
            // Stretch image to fill screen
            RectTransform rt = imageObj.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;

            // Add CanvasGroup
            fadeCanvasGroup = imageObj.AddComponent<CanvasGroup>();
            fadeCanvasGroup.alpha = 0f; // Start transparent
            fadeCanvasGroup.blocksRaycasts = false; // Don't block clicks when transparent
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        // Fade Out (Transparent to Black)
        fadeCanvasGroup.blocksRaycasts = true; // Block input
        yield return StartCoroutine(Fade(0f, 1f));

        // Load Scene
        SceneManager.LoadScene(sceneName);

        // Wait for scene to load (optional, but good for stability)
        yield return null; 

        // Fade In (Black to Transparent)
        yield return StartCoroutine(Fade(1f, 0f));
        fadeCanvasGroup.blocksRaycasts = false; // Unblock input
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}
