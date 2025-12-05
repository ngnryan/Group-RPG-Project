using UnityEngine;

public class UnderwaterTrigger : MonoBehaviour
{
    private FogManager fogManager;

    void Start()
    {
        fogManager = Object.FindFirstObjectByType<FogManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogManager.ApplyUnderwaterFog();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogManager.ApplyDefaultFog();
        }
    }
}
