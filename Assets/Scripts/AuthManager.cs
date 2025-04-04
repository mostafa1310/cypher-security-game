using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    // Keys for PlayerPrefs
    private const string IsLoggedInKey = "IsLoggedIn";

    void Start()
    {
        // Check if user is logged in (default is false)
        bool isLoggedIn = PlayerPrefs.GetInt(IsLoggedInKey, 0) == 1;

        if (isLoggedIn)
        {
            // Navigate to the MainMenu scene if already signed in
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            // Otherwise, load the Login screen
            SceneManager.LoadScene("Login");
        }
    }

    // Call this method on successful login
    public static void SetLoggedIn()
    {
        PlayerPrefs.SetInt(IsLoggedInKey, 1);
        PlayerPrefs.Save();
    }

    // Optionally, call this method on logout
    public static void SetLoggedOut()
    {
        PlayerPrefs.SetInt(IsLoggedInKey, 0);
        PlayerPrefs.Save();
    }
}
