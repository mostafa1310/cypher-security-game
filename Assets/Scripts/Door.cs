using UnityEngine;
using System.Collections;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openAngle = 90f; // angle to open
    [SerializeField] private float openSpeed = 2f;    // speed of opening animation
    [SerializeField] private GameObject passwordPanel; // UI panel for password input
    [SerializeField] private TMP_InputField passwordInput; // input field for password
    [SerializeField] private TMP_Text feedbackText; // text to display feedback message

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool isAnimating = false;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
    }

    public void Interact()
    {
        if (!isOpen && !isAnimating)
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

    // Call this method from the UI submit button.
    public void SubmitPassword()
    {
        // Assuming Pass_Generator.GeneratedPassword holds the correct password.
        if (passwordInput.text == Pass_Generator.GeneratedPassword)
        {
            if (passwordPanel != null)
                passwordPanel.SetActive(false);
            InteractionManager.IsInteractionActive = false;
            gameObject.tag = "Untagged";
            StartCoroutine(OpenDoor());
        }
        else
        {
            if (feedbackText != null)
                feedbackText.text = "Wrong password!";
        }
    }

    // Call this method from a UI cancel button to exit password input.
    public void CancelInteraction()
    {
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }

    private IEnumerator OpenDoor()
    {
        isAnimating = true;
        while (Quaternion.Angle(transform.rotation, openRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, openRotation, openSpeed * Time.deltaTime * 100);
            yield return null;
        }
        transform.rotation = openRotation;
        isOpen = true;
        isAnimating = false;
    }
}
