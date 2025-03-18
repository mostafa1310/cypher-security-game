using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Authenticate_Safe : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject AuthenticatePanel; // UI panel for password input
    [SerializeField] private TMP_InputField AuthenticateInput; // input field for password
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message
    [SerializeField] Vault vault;
    [SerializeField] private Email_Manager emailManager; // reference to email manager to send code
    private bool isEmailSent = false;
    private bool isEmailVerified = false;
    private string sentCode = "";
    public string Name = "Open";

    string IInteractable.Name { get => Name; set => Name = value; }

    public void Interact()
    {
        if (!vault.isOpen && !vault.isAnimating)
        {
            InteractionManager.IsInteractionActive = true;

            // StartCoroutine(storyTeller.Send_message(new List<string> { "قبل أن تخطو أولى خطواتك في هذه الرحلة الغامضة، عليك فتح البوابة الأولى. لإنجاز ذلك، أدخل كلمة مرورك الخاصة—أمر بسيط، أليس كذلك؟ بمجرد انتهائك، ستبدأ مغامرتك الحقيقية!" }));
            if (AuthenticatePanel != null)
                AuthenticatePanel.SetActive(true);
            if (AuthenticateInput != null)
                AuthenticateInput.text = "";
            if (feedbackText != null)
                feedbackText.text = "";
        }
    }
    public void CancelInteraction()
    {
        if (AuthenticatePanel != null)
            AuthenticatePanel.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AuthenticatePanel != null)
            AuthenticatePanel.SetActive(false);
#if UNITY_EDITOR
        print($"Password: {PlayerPrefs.GetString("Password")}");
        print($"Passcode: {PlayerPrefs.GetString("Passcode")}");
#endif
    }
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Generates a random 6-digit code.
    private string GenerateRandomCode()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(100000, 1000000).ToString();
    }

    public void SubmitAuthenticate()
    {
        if (!isEmailSent)
        {
            // Check if the entered email is valid.
            if (AuthenticateInput.text != PlayerPrefs.GetString("Password"))
            {
                if (feedbackText != null)
                    feedbackText.text = "Invalid password!";
                return;
            }
            // Email is valid; generate a verification code.
            sentCode = GenerateRandomCode();
            // Send the code using the Email_Manager (e.g., by adding an email with the code).
            if (emailManager != null)
            {
                string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
                emailManager.add_email("Verification Code", "Your code is: " + sentCode, currentDate);
                Debug.Log($"email code is:{sentCode}");
            }
            else
            {
                Debug.Log("Email Manager is not assigned!");
                return;
            }
            isEmailSent = true;
            if (feedbackText != null)
                feedbackText.text = "Verification code sent. Please enter the code.";
            AuthenticateInput.text = "";
            AuthenticateInput.placeholder.GetComponent<TMP_Text>().text = "Enter verification code";
        }
        else
        {
            if (!isEmailVerified)
            {
                // Now verify the entered code.
                if (AuthenticateInput.text == sentCode)
                {
                    if (feedbackText != null)
                        feedbackText.text = "Verification successful!";
                    isEmailVerified = true;
                    AuthenticateInput.text = "";
                    AuthenticateInput.placeholder.GetComponent<TMP_Text>().text = "Enter Your Passcode:";
                }
                else
                {
                    if (feedbackText != null)
                        feedbackText.text = "Invalid code!";
                }
            }
            else
            {
                if (AuthenticateInput.text == PlayerPrefs.GetString("Passcode"))
                {
                    if (AuthenticatePanel != null)
                        AuthenticatePanel.SetActive(false);
                    InteractionManager.IsInteractionActive = false;
                    StartCoroutine(vault.OpenDoor());
                    gameObject.tag = "Untagged";
                }
                else
                {
                    if (feedbackText != null)
                        feedbackText.text = "Wrong Passcode!";
                }
            }
        }
    }
}
