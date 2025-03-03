using UnityEngine;
using TMPro;
using System;

public class Password_submit : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject passwordPanel; // UI panel for password input
    [SerializeField] private TMP_InputField passwordInput; // input field for password
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message
    Door door;

    public string Name = "Open";

    string IInteractable.Name { get => Name; set => Name = value; }

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
    public void Submit_Password()
    {
        string password = passwordInput.text;
        password = password.Trim();
        if (password.Length == 0)
        {
            if (feedbackText != null)
                feedbackText.text = "Please enter a password!";
            return;
        }
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
        PlayerPrefs.SetString("password", passwordInput.text);
        InteractionManager.IsInteractionActive = false;
        gameObject.tag = "Untagged";
        StartCoroutine(door.OpenDoor());
    }

    void Start()
    {
        door = GetComponent<Door>();
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
    }
}
