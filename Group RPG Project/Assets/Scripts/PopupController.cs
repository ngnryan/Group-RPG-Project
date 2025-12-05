using UnityEngine;

public class PopupController : MonoBehaviour
{
    [Tooltip("Assign the UI Image GameObject here.")]
    public GameObject popupImage;

    void Start()
    {
        // Ensure the popup is visible when the scene starts, UNLESS we just lost a battle
        if (popupImage != null)
        {
            if (PlayerPrefs.GetInt("BattleLost", 0) == 1)
            {
                popupImage.SetActive(false);
                // Optional: Reset the flag if you want it to show next time, 
                // but usually you might want to keep it hidden or reset it elsewhere.
                // For now, we'll keep it as is or reset it if needed.
                // PlayerPrefs.SetInt("BattleLost", 0); 
            }
            else
            {
                popupImage.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("PopupController: Popup Image is not assigned!");
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
