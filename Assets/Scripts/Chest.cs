using UnityEngine;
using System.Collections;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f; // angle to open
    [SerializeField] private float openSpeed = 2f;    // speed of opening animation


    private Quaternion closedRotation;
    private Quaternion openRotation;
    public bool isOpen = false;
    public bool isAnimating = false;

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
    }
}
