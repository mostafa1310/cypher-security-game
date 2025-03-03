using UnityEngine;
using System.Collections;
using TMPro;

public class StoryTeller : MonoBehaviour
{
    [SerializeField] private TypewriterEffect typewriterEffect; // assign the TypewriterEffect component
    [SerializeField] private string[] storyMessages; // sample story messages

    private int currentMessage = 0;

    void Start()
    {
        // Disable player actions globally
        InteractionManager.IsInteractionActive = true;
        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        while (currentMessage < storyMessages.Length)
        {
            typewriterEffect.ShowMessage(storyMessages[currentMessage]);
            // wait until the user touches/clicks the screen to proceed
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            currentMessage++;
            yield return null;
        }
        // Story finished, clear text and re-enable player actions
        typewriterEffect.storyText.text = "";
        InteractionManager.IsInteractionActive = false;
    }
}
