using UnityEngine;
using TMPro;
using System.Collections;
using RTLTMPro;

public class TypewriterEffect : MonoBehaviour
{
    public RTLTextMeshPro storyText; // assign your UI TMP_Text element in the inspector
    [SerializeField] private float charDelay = 0.05f; // delay between characters

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        TypeText(message);
    }

    private void TypeText(string message)
    {
        storyText.text = "";
        // foreach (char c in message)
        // {
        //     storyText.text += c;
        //     yield return new WaitForSeconds(charDelay);
        // }
        storyText.text = message;

    }
}
