using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the start button is clicked.")]
    public string sceneToLoad = "SampleScene";

    /// <summary>
    /// Loads the specified open world scene.
    /// Attach this method to the Start Button's OnClick event.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
