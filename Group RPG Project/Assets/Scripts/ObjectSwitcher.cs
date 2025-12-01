using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    // An array of GameObjects to make disappear
    public GameObject[] objectsToHide;

    // An array of GameObjects to make appear
    public GameObject[] objectsToShow;

    void Start()
    {
        // Initially hide the objects that are meant to pop up
        // We check if the object is not null before trying to deactivate it.
        if (objectsToShow != null)
        {
            foreach (GameObject obj in objectsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    // This public method can be triggered by another script or a UI event
    public void SwitchObjects()
    {
        // Hide the specified objects
        if (objectsToHide != null)
        {
            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        // Show the specified objects
        if (objectsToShow != null)
        {
            foreach (GameObject obj in objectsToShow)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
