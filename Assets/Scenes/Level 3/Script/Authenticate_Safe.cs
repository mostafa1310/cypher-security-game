using System;
using System.Collections.Generic;
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
    private bool isPasscodeVerified = false;
    private bool isPasswordVerified = false;
    private string sentCode = "";
    public string Name = "Open";

    string IInteractable.Name { get => Name; set => Name = value; }
    [SerializeField] private StoryTeller storyTeller; // assign the StoryTeller script
    [SerializeField] bool first_interact = false;

    public void Interact()
    {
        if (!first_interact)
        {
            first_interact = true;
            StartCoroutine(storyTeller.Send_message(new List<string> { "لإتمام العملية، عليك إدخال كلمة مرور قوية. تذكر تلك التي أنشأتها سابقًا؟ حان وقت استخدامها!" }));
        }
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
        print($"Password: {PlayerPrefs.GetString("Password")}");
        print($"Passcode: {PlayerPrefs.GetString("Passcode")}");
        // StartCoroutine(storyTeller.Send_message(new List<string> { "بعد رحلتك الطويلة، وصلت أخيرًا إلى البنك، حيث سيتم تأمين كنزك إلى الأبد. ولكن قبل أن تتمكن من ذلك، عليك اجتياز التحقق الأخير!" }));
    }

    // Generates a random 6-digit code.
    private string GenerateRandomCode()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(100000, 1000000).ToString();
    }

    public void SubmitAuthenticate()
    {
        if (!isPasswordVerified && !isEmailSent)
        {
            // Check if the entered email is valid.
            if (AuthenticateInput.text != PlayerPrefs.GetString("Password"))
            {
                if (feedbackText != null)
                    feedbackText.text = "Invalid password!";
                return;
            }
            isPasswordVerified = true;
            AuthenticateInput.placeholder.GetComponent<TMP_Text>().text = "Enter passcode";
            AuthenticateInput.text = "";
            StartCoroutine(storyTeller.Send_message(new List<string> { "الآن، أدخل رمز الأمان (Passcode) الذي أنشأته في السفينة." }));
            if (feedbackText != null)
                feedbackText.text = "Password verified!";
        }
        else if (!isPasscodeVerified && !isEmailSent)
        {
            if (AuthenticateInput.text == PlayerPrefs.GetString("Passcode"))
            {
                isPasscodeVerified = true;
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
                StartCoroutine(storyTeller.Send_message(new List<string> { "خطوة أخيرة! لإثبات هويتك، عليك التحقق من بريدك الإلكتروني عبر نظام (Email Authentication)." }));
            }
            else
            {
                if (feedbackText != null)
                    feedbackText.text = "Wrong Passcode!";
            }
        }
        else if (isEmailSent)
        {
            if (AuthenticateInput.text == sentCode)
            {
                if (AuthenticatePanel != null)
                    AuthenticatePanel.SetActive(false);
                InteractionManager.IsInteractionActive = false;
                StartCoroutine(vault.OpenDoor());
                gameObject.tag = "Untagged";
                StartCoroutine(storyTeller.Send_message(new List<string> { "مذهل! لقد أثبت هويتك بنجاح، وتم تأمين كنزك في البنك. مغامرتك انتهت بنجاح، ولكن هل أنت مستعد لتحدٍ جديد؟" }, new List<Action> { () => { vault.open_callback.Invoke(); PlayerPrefs.SetInt("HighestLevelCompleted", 3); } }));
            }
            else
            {
                if (feedbackText != null)
                    feedbackText.text = "Invalid code!";
            }
        }

    }
}
