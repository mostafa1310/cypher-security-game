using UnityEngine;
using TMPro;

public class Pass_Generator : MonoBehaviour
{
    [SerializeField] private GameObject pass_texts;
    public static string GeneratedPassword; // now accessible globally

    void Start()
    {
        GeneratedPassword = "";
        int childCount = pass_texts.transform.childCount;
        string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        for (int i = 0; i < childCount; i++)
        {
            char randChar = validChars[Random.Range(0, validChars.Length)];

            // Randomly set letter to lower case if applicable
            if (char.IsLetter(randChar) && Random.Range(0, 2) == 0)
            {
                randChar = char.ToLower(randChar);
            }

            GeneratedPassword += randChar;

            Transform child = pass_texts.transform.GetChild(i);
            TMP_Text tmpTextComponent = child.GetComponent<TMP_Text>();
            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = $"{(i + 1)}\n{randChar}";
            }
        }
        Debug.Log("Generated Password: " + GeneratedPassword);
    }
}
