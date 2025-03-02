using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Public variable to set the scene name in the Inspector
    public string sceneName;

    // Function to change scene by name
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }

    // Example: Change scene on button click
    public void ChangeScene(string newSceneName)
    {
        if (!string.IsNullOrEmpty(newSceneName))
        {
            SceneManager.LoadScene(newSceneName);
        }
        else
        {
            Debug.LogError("Provided scene name is empty!");
        }
    }

    // Function to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
