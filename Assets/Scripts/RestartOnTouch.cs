using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnTouch : MonoBehaviour
{
    // This function will be called when another collider enters the trigger zone (if isTrigger is enabled)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the colliding object has the tag "Player" (adjust as necessary)
        {
            RestartScene();
        }
    }

    // This function will be called when another collider collides with this object (if isTrigger is not enabled)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))  // Check if the colliding object has the tag "Player" (adjust as necessary)
        {
            RestartScene();
        }
    }

    // Function to restart the current scene
    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
