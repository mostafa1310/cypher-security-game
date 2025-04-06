using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // added for scene loading

public class FirebaseSignUp : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField fullNameField;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_Text feedbackText;

    private FirebaseAuth auth;

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

    public void OnSignUpButtonClicked()
    {
        string fullName = fullNameField.text.Trim();
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();
        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "All fields are required.";
            return;
        }
        StartCoroutine(SignUp(fullName, email, password));
    }

    IEnumerator SignUp(string fullName, string email, string password)
    {
        var signUpTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => signUpTask.IsCompleted);
        if (signUpTask.Exception != null)
        {
            print(signUpTask.Exception.ToString());
            feedbackText.text = "Sign Up Failed";
        }
        else
        {
            FirebaseUser newUser = signUpTask.Result.User;
            UserProfile profile = new UserProfile { DisplayName = fullName };
            var profileTask = newUser.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(() => profileTask.IsCompleted);
            if (profileTask.Exception != null)
            {
                feedbackText.text = "Display name update failed";
            }
            else
            {
                feedbackText.text = "Sign Up Successful!";
                AuthManager.SetLoggedIn(); // Set the logged-in state in PlayerPrefs
                // Optionally load the start game screen after sign up.
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}