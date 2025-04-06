using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // added for scene loading

public class FirebaseLogin : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_Text feedbackText;

    private FirebaseAuth auth;
    [SerializeField] private int StartGameScreen;

    IEnumerator Start()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => dependencyTask.IsCompleted);
        if (dependencyTask.Result == DependencyStatus.Available)
        {
            auth = FirebaseAuth.DefaultInstance;
            // Check if user is already logged in, if so, go to start game screen.
            // if (auth.CurrentUser != null)
            // {
            //     AuthManager.SetLoggedIn(); // Set the logged-in state in PlayerPrefs
            //     SceneManager.LoadScene("MainMenu"); // adjust scene name as needed
            //     yield break; // Exit Start coroutine
            // }
        }
        else
        {
            Debug.LogError("Firebase dependency error: " + dependencyTask.Result);
        }
    }

    public void OnLoginButtonClicked()
    {
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Email and password must not be empty.";
            return;
        }
        StartCoroutine(Login(email, password));
    }

    IEnumerator Login(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);
        if (loginTask.Exception != null)
        {
            feedbackText.text = "Login Failed";
        }
        else
        {
            feedbackText.text = "Login Successful!";
            AuthManager.SetLoggedIn(); // Set the logged-in state in PlayerPrefs
            // Optionally load the start game screen here as well.
            SceneManager.LoadScene("MainMenu"); // adjust scene name as needed
        }
    }
}