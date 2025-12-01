using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the rendering order of a UI Image by adjusting its sibling index in the hierarchy.
/// This provides "button-like" booleans in the Inspector to move the element.
/// </summary>
[RequireComponent(typeof(Image))]
public class ImageLayerController : MonoBehaviour
{
    [Tooltip("Check this to bring the image to the front of its siblings.")]
    public bool bringToFront;

    [Tooltip("Check this to send the image to the back of its siblings.")]
    public bool sendToBack;

    // OnValidate is called in the editor when the script is loaded or a value is changed in the Inspector.
    void OnValidate()
    {
        if (bringToFront)
        {
            transform.SetAsLastSibling();
            bringToFront = false;
        }

        if (sendToBack)
        {
            transform.SetAsFirstSibling();
            sendToBack = false;
        }
    }

    /// <summary>
    /// Moves the UI element to the front (rendered on top of siblings).
    /// </summary>
    public void BringToFront()
    {
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// Moves the UI element to the back (rendered behind siblings).
    /// </summary>
    public void SendToBack()
    {
        transform.SetAsFirstSibling();
    }
}
