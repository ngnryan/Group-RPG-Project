using UnityEngine;

public class PopupController : MonoBehaviour
{
    [Tooltip("Assign the UI Image GameObject here.")]
    public GameObject popupImage;

    void Start()
    {
        if (popupImage == null)
        {
            Debug.LogWarning("PopupController: Popup Image is not assigned!");
            return;
        }

        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Special logic for Moonpaw Veil: Only show if coming from Intro
        if (currentScene == "Moonpaw Veil")
        {
            if (PlayerPrefs.GetInt("ComingFromIntro", 0) == 1)
            {
                popupImage.SetActive(true);
                // Consume the flag so it doesn't show again on reload/return
                PlayerPrefs.SetInt("ComingFromIntro", 0);
                PlayerPrefs.Save();
            }
            else
            {
                popupImage.SetActive(false);
            }
        }
        else
        {
            // Default behavior for other scenes (or if we want it to show elsewhere)
            // Ensure the popup is visible when the scene starts, UNLESS we just lost a battle
            if (PlayerPrefs.GetInt("BattleLost", 0) == 1)
            {
                popupImage.SetActive(false);
            }
            else
            {
                popupImage.SetActive(true);
            }
        }
    }

    void Update()
    {
        // Check for left mouse button click or screen tap
        if (Input.GetMouseButtonDown(0))
        {
            if (popupImage != null && popupImage.activeSelf)
            {
                popupImage.SetActive(false);
            }
        }
    }
}
