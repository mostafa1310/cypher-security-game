using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f; // angle to open
    [SerializeField] private float openSpeed = 2f;    // speed of opening animation


    private Quaternion closedRotation;
    private Quaternion openRotation;
    public bool isOpen = false;
    public bool isAnimating = false;
    public UnityEvent open_callback;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(openAngle, 0, 0));

    }
    public IEnumerator OpenDoor()
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
        open_callback.Invoke();
        // End_panel.SetActive(true);
    }
}
