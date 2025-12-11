using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the start button is clicked.")]
    public string sceneToLoad = "SampleScene";

    private void Start()
    {
        // Reset the battle lost flag when the game starts (Intro Scene)
        PlayerPrefs.SetInt("BattleLost", 0);
        PlayerPrefs.SetInt("LostToEvilCatMaster", 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the specified open world scene.
    /// Attach this method to the Start Button's OnClick event.
    /// </summary>
    public void StartGame()
    {
        // Set flag to indicate we are coming from the Intro scene
        PlayerPrefs.SetInt("ComingFromIntro", 1);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(sceneToLoad);
    }
}
