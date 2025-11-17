using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProximitySound : MonoBehaviour
{
    [Tooltip("The player or object that will trigger the sound to get louder.")]
    public Transform listenerTransform;

    [Tooltip("The maximum distance at which the sound can be heard.")]
    public float maxDistance = 20.0f;

    [Tooltip("The distance at which the sound is at its loudest.")]
    public float minDistance = 2.0f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Start with the volume at 0 so it's not audible until the player is in range.
        audioSource.volume = 0;
    }

    void Start()
    {
        if (listenerTransform == null)
        {
            Debug.LogError("ProximitySound: Listener Transform has not been assigned. Finding object with 'Player' tag.");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                listenerTransform = player.transform;
            }
            else
            {
                Debug.LogError("ProximitySound: Could not find a GameObject with the 'Player' tag. Disabling script.");
                enabled = false;
            }
        }
    }

    void Update()
    {
        if (listenerTransform == null) return;

        // Calculate the distance between this object and the listener
        float distance = Vector3.Distance(transform.position, listenerTransform.position);

        // Calculate the volume
        // InverseLerp gives us a value between 0 and 1.
        // If distance is maxDistance, value is 0 (silent).
        // If distance is minDistance, value is 1 (loudest).
        float volume = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // Clamp the value to ensure it's between 0 and 1
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
