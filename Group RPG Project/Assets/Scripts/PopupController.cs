using UnityEngine;

public class PopupController : MonoBehaviour
{
    [Tooltip("Assign the UI Image GameObject here.")]
    public GameObject popupImage;

    void Start()
    {
        // Ensure the popup is visible when the scene starts
        if (popupImage != null)
        {
            popupImage.SetActive(true);
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
