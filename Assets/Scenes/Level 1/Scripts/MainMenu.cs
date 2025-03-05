using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
   
    public void PlayGame()
    {
        SceneManager.LoadScene("level selection");
    }

    public void Options()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Optionsback()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING");
        Application.Quit();
    }

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panel != null)
            {
                panel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor
                Time.timeScale = 1f; // Ensure the game isn't paused
            }
        }
    }

    private void Update()
    {
        if (panel != null && panel.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
