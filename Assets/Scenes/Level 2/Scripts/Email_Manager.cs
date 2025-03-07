using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email_Manager : MonoBehaviour
{
    [SerializeField] List<Email> emails = new List<Email>();
    public GameObject emailPrefab_overview;
    public Transform emailsParent_menu;
    public Transform emailsList_menu;
    public Transform emailParent_view;
    private void Start()
    {
        add_email("Welcome to the game", "Welcome to the game, we hope you enjoy it", "01/01/2021");
        add_email("New level unlocked", "You have unlocked a new level", "02/01/2021");
    }
    public void open_mail_menu()
    {
        InteractionManager.IsInteractionActive = true;
        emailsParent_menu.gameObject.SetActive(true);
        foreach (Transform child in emailsList_menu.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Email email in emails)
        {
            GameObject emailObject = Instantiate(emailPrefab_overview, emailsList_menu);
            emailObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = email.subject;
            emailObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = email.date;
            Button button = emailObject.GetComponent<Button>();
            button.onClick.AddListener(delegate { open_email(email); });
        }
    }
    public void CancelInteraction()
    {
        InteractionManager.IsInteractionActive = false;
    }
    public void open_email(Email email)
    {
        emailParent_view.gameObject.SetActive(true);
        emailsParent_menu.gameObject.SetActive(false);
        emailParent_view.GetChild(0).GetComponent<TMPro.TMP_Text>().text = email.subject;
        emailParent_view.GetChild(1).GetComponent<TMPro.TMP_Text>().text = email.date;
        emailParent_view.GetChild(2).GetComponent<TMPro.TMP_Text>().text = email.body;
    }
    public void add_email(string subject, string body, string date)
    {
        emails.Add(new Email(subject, body, date));
    }
}
[Serializable]
public class Email
{
    public string subject;
    public string body;
    public string date;
    public Email(string subject, string body, string date)
    {
        this.subject = subject;
        this.body = body;
        this.date = date;
    }
}
