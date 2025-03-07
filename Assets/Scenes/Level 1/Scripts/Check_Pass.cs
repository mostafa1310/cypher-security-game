using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Check_Pass : MonoBehaviour, IInteractable
{
    Door door;
    [SerializeField] private GameObject passwordPanel; // UI panel for password input
    [SerializeField] private TMP_InputField passwordInput; // input field for password
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message

    public string Name = "Open";

    string IInteractable.Name { get => Name; set => Name = value; }
    [SerializeField] private StoryTeller storyTeller; // assign the StoryTeller script

    void Start()
    {
        door = GetComponent<Door>();
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
    }
    public void Interact()
    {
        if (!door.isOpen && !door.isAnimating)
        {
            InteractionManager.IsInteractionActive = true;
            if (passwordPanel != null)
                passwordPanel.SetActive(true);
            if (passwordInput != null)
                passwordInput.text = "";
            if (feedbackText != null)
                feedbackText.text = "";
        }
    }
    public void CancelInteraction()
    {
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }

    // Call this method from the UI submit button.
    public void SubmitPassword()
    {
        // Assuming Pass_Generator.GeneratedPassword holds the correct password.
        if (passwordInput.text == Pass_Generator.GeneratedPassword)
        {
            if (passwordPanel != null)
                passwordPanel.SetActive(false);
            StartCoroutine(storyTeller.Send_message(new List<string> { "تهانينا! لقد اجتزت التحدي الأول وأثبت جدارتك كمغامر حقيقي. لكن لا يزال أمامك طريق طويل… والآن، حان الوقت لاختبار ما تعلمته! أدخل كلمة مرور جديدة وفقًا لما اكتسبته من معرفة، ثم استعد للمرحلة التالية!" }));
            InteractionManager.IsInteractionActive = false;
            gameObject.tag = "Untagged";
            StartCoroutine(door.OpenDoor());
        }
        else
        {
            if (feedbackText != null)
                feedbackText.text = "Wrong password!";
        }
    }
}
