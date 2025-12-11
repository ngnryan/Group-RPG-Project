using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterController : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the player gets close.")]
    public string battleSceneName = "BattleScene";

    [Tooltip("The distance at which the encounter triggers.")]
    public float encounterRadius = 2.0f;

    [Tooltip("The Player's Transform. If empty, the script will try to find it automatically.")]
    public Transform playerTransform;

    [Header("Conditional Settings")]
    [Tooltip("If true, checks valid PlayerPref key to alternate scene.")]
    public bool useConditionalScene = false;

    [Tooltip("The alternate scene to load if the condition is met.")]
    public string alternateBattleSceneName = "BattleScene_Revisit";

    [Tooltip("The PlayerPref key to check. Default is 'LostToEvilCatMaster'.")]
    public string conditionKey = "LostToEvilCatMaster";

    void Start()
    {
        // If playerTransform is already assigned in the inspector, we don't need to find it.
        if (playerTransform != null) return;

        // Try to find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        // Fallback: Try to find by name if tag fails
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        
        // Fallback: Try to find by name "MC" (common name)
        if (player == null)
        {
            player = GameObject.Find("MC");
        }

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("EncounterController: Could not find Player object! Please ensure the Player object is tagged 'Player', named 'Player', or assign the Player Transform manually in the Inspector.");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Check distance
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= encounterRadius)
        {
            // Trigger encounter
            string sceneToLoad = battleSceneName;

            if (useConditionalScene)
            {
               int prefValue = PlayerPrefs.GetInt(conditionKey, 0);
               Debug.Log($"[EncounterController] Conditional check enabled. Key: '{conditionKey}', Value: {prefValue}");
               
               if (prefValue == 1)
               {
                   sceneToLoad = alternateBattleSceneName;
                   Debug.Log($"[EncounterController] Condition met! Switching scene to: '{sceneToLoad}'");
               }
               else 
               {
                   Debug.Log($"[EncounterController] Condition NOT met. Loading default scene: '{sceneToLoad}'");
               }
            }

            if (SceneTransitionManager.Instance != null)
            {
                SceneTransitionManager.Instance.LoadScene(sceneToLoad);
            }
            else
            {
                // Fallback if manager is missing
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
