using TMPro;
using UnityEngine;
using System.Text.RegularExpressions; // added for email validation
using System;
using System.Collections.Generic;

public class Register_Ticket : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject EmailPanal; // UI panel for email input and verification code input
    [SerializeField] private TMP_InputField EmailInput; // input field for email or code
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message
    [SerializeField] private Email_Manager emailManager; // reference to email manager to send code

    public string Name = "Buy";

    string IInteractable.Name { get => Name; set => Name = value; }

    private bool isEmailSent = false;
    private string sentCode = "";
    [SerializeField] private Fence fence;
    [SerializeField] private StoryTeller storyTeller; // assign the StoryTeller script
    [SerializeField] bool first_interact = false;

    void Start()
    {
        if (EmailPanal != null)
            EmailPanal.SetActive(false);
    }

    public void Interact()
    {
        if (!first_interact)
        {
            first_interact = true;
            StartCoroutine(storyTeller.Send_message(new List<string> { "مرحبًا أيها المغامر! قبل أن تحصل على تذكرتك، عليك إدخال بيانات بريدك الإلكتروني." }));
        }
        InteractionManager.IsInteractionActive = true;
        if (EmailPanal != null)
            EmailPanal.SetActive(true);
        if (EmailInput != null)
            EmailInput.text = "";
        if (feedbackText != null)
            feedbackText.text = "";
    }

    public void CancelInteraction()
    {
        if (EmailPanal != null)
            EmailPanal.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }

    // Validates if the input string is a valid email.
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

    // Call this method from the UI submit button.
    public void SubmitEmail()
    {
        if (!isEmailSent)
        {
            // Check if the entered email is valid.
            if (!IsValidEmail(EmailInput.text.Trim()))
            {
                if (feedbackText != null)
                    feedbackText.text = "Entered email is not valid!";
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
            isEmailSent = true;
            if (feedbackText != null)
                feedbackText.text = "Verification code sent. Please enter the code.";
            EmailInput.text = "";
            EmailInput.placeholder.GetComponent<TMP_Text>().text = "Enter verification code";
            StartCoroutine(storyTeller.Send_message(new List<string> { "لقد وصلتك رسالة تحتوي على رمز الدخول. أدخله الآن للحصول على تذكرتك." }));
        }
        else
        {
            // Now verify the entered code.
            if (EmailInput.text == sentCode)
            {
                // StartCoroutine(storyTeller.Send_message(new List<string> { "رائع! لقد حصلت على تذكرتك. يمكنك الآن الصعود إلى السفينة!" }));
                if (EmailPanal != null)
                    EmailPanal.SetActive(false);
                InteractionManager.IsInteractionActive = false;
                StartCoroutine(fence.OpenDoor());
                gameObject.tag = "Untagged";
                StartCoroutine(storyTeller.Send_message(new List<string> { "رائع! لقد حصلت على تذكرتك. يمكنك الآن الصعود إلى السفينة!", "ها قد وصلت إلى داخل السفينة، وبينما تتجول، تكتشف شيئًا مذهلًا… صندوق كنز مغلق بآلية أمان مشددة!" }));
            }
            else
            {
                if (feedbackText != null)
                    feedbackText.text = "Wrong verification code!";
                StartCoroutine(storyTeller.Send_message(new List<string> { "عذرًا، هذا الرمز غير صحيح! عليك البحث عن حل آخر." }));
            }
        }
    }
}
