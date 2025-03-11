using TMPro;
using UnityEngine;
using System.Text.RegularExpressions; // added for email validation
using System;
using System.Collections.Generic;

public class Passcode_Register : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject PasscodePanel; // UI panel for password input
    [SerializeField] private TMP_InputField PasscodeInput; // input field for password
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message
    [SerializeField] Chest chest;

    public string Name = "Open";

    string IInteractable.Name { get => Name; set => Name = value; }
    // [SerializeField] private StoryTeller storyTeller; // assign the StoryTeller script
    public void Interact()
    {
        if (!chest.isOpen && !chest.isAnimating)
        {
            InteractionManager.IsInteractionActive = true;

            // StartCoroutine(storyTeller.Send_message(new List<string> { "قبل أن تخطو أولى خطواتك في هذه الرحلة الغامضة، عليك فتح البوابة الأولى. لإنجاز ذلك، أدخل كلمة مرورك الخاصة—أمر بسيط، أليس كذلك؟ بمجرد انتهائك، ستبدأ مغامرتك الحقيقية!" }));
            if (PasscodePanel != null)
                PasscodePanel.SetActive(true);
            if (PasscodeInput != null)
                PasscodeInput.text = "";
            if (feedbackText != null)
                feedbackText.text = "";
        }
    }
    public void CancelInteraction()
    {
        if (PasscodePanel != null)
            PasscodePanel.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }
    public void Submit_Passcode()
    {
        string password = PasscodeInput.text;
        password = password.Trim();
        if (password.Length == 0)
        {
            if (feedbackText != null)
                feedbackText.text = "Please enter a password!";
            return;
        }
        if (PasscodePanel != null)
            PasscodePanel.SetActive(false);
        PlayerPrefs.SetString("Passcode", PasscodeInput.text);
        InteractionManager.IsInteractionActive = false;
        gameObject.tag = "Untagged";
        StartCoroutine(chest.OpenDoor());
    }

    void Start()
    {
        if (PasscodePanel != null)
            PasscodePanel.SetActive(false);
    }
}
