using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    void Awake()
    {
        // If an instance of this already exists and it's not this one, destroy this one.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        // This is the first instance, so make it the singleton.
        instance = this;
        
        // Don't destroy this object when a new scene loads.
        DontDestroyOnLoad(this.gameObject);
    }
}
