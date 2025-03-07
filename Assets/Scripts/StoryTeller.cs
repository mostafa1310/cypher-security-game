using UnityEngine;
using System.Collections;
using TMPro;
using ArabicSupport;
using System.Collections.Generic;

public class StoryTeller : MonoBehaviour
{
    [SerializeField] private TypewriterEffect typewriterEffect_prefab; // assign the TypewriterEffect component
    [SerializeField] private TypewriterEffect typewriterEffect; // assign the TypewriterEffect component
    [SerializeField] private Canvas UI_Parent;
    [SerializeField] private string[] storyMessages; // sample story messages

    private int currentMessage = 0;

    void Start()
    {
        // Disable player actions globally
        InteractionManager.IsInteractionActive = true;
        typewriterEffect = Instantiate(typewriterEffect_prefab, UI_Parent.transform);
        typewriterEffect.gameObject.SetActive(false);
        // for (int i = 0; i < storyMessages.Length; i++)
        // {
        //     storyMessages[i] = ArabicFixer.Fix(storyMessages[i]);
        // }
        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        typewriterEffect.gameObject.SetActive(true);
        while (currentMessage < storyMessages.Length)
        {
            typewriterEffect.ShowMessage(storyMessages[currentMessage]);
            // wait until the user touches the screen (mobile)
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
            currentMessage++;
            yield return null;
        }
        // Story finished, clear text and re-enable player actions
        typewriterEffect.storyText.text = "";
        typewriterEffect.gameObject.SetActive(false);
        InteractionManager.IsInteractionActive = false;
    }
    public IEnumerator Send_message(List<string> messages)
    {
        typewriterEffect.gameObject.SetActive(true);
        int currentMessage_index = 0;
        while (currentMessage_index < messages.Count)
        {
            typewriterEffect.ShowMessage(messages[currentMessage_index]);
            // wait until the user touches the screen (mobile)
            yield return new WaitUntil(() => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
            currentMessage_index++;
            yield return null;
        }
        typewriterEffect.storyText.text = "";
        typewriterEffect.gameObject.SetActive(false);
    }
}
