using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text storyText; // assign your UI TMP_Text element in the inspector
    [SerializeField] private float charDelay = 0.05f; // delay between characters

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        storyText.text = "";
        foreach (char c in message)
        {
            storyText.text += c;
            yield return new WaitForSeconds(charDelay);
        }
    }
}
