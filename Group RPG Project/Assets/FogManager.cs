using UnityEngine;

public class FogManager : MonoBehaviour
{
    [Header("Default Fog Settings")]
    public Color defaultFogColor = new Color(0.5f, 0.5f, 0.5f);
    public float defaultFogDensity = 0.002f;

    [Header("Underwater Fog Settings")]
    public Color underwaterFogColor = new Color(0.1f, 0.3f, 0.4f);
    public float underwaterFogDensity = 0.04f;

    private bool isUnderwater = false;

    void Start()
    {
        ApplyDefaultFog();
    }

    public void ApplyUnderwaterFog()
    {
        RenderSettings.fogColor = underwaterFogColor;
        RenderSettings.fogDensity = underwaterFogDensity;
        isUnderwater = true;
    }

    public void ApplyDefaultFog()
    {
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        isUnderwater = false;
    }

    public bool IsUnderwater()
    {
        return isUnderwater;
    }
}