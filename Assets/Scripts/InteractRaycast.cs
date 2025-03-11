using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // assign the main camera in the inspector
    [SerializeField] private Button interactionPrompt; // assign a TMP Text UI element (e.g., "E" prompt)
    [SerializeField] private float interactDistance = 5f; // max raycast distance


    void Update()
    {
        if (InteractionManager.IsInteractionActive || Email_Manager.Email_Active)
        {
            interactionPrompt.gameObject.SetActive(false);
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // print(hit.collider.name);
            if (hit.collider.CompareTag("Interactable"))
            {
                // Show interaction prompt
                interactionPrompt.transform.GetChild(0).GetComponent<TMP_Text>().text = hit.collider.GetComponent<IInteractable>().Name;
                interactionPrompt.gameObject.SetActive(true);
                interactionPrompt.onClick.RemoveAllListeners();
                interactionPrompt.onClick.AddListener(delegate { hit.collider.GetComponent<IInteractable>()?.Interact(); });
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<IInteractable>()?.Interact();
                }
                return;
            }
        }
        // Hide interaction prompt if no interactable object is hit
        interactionPrompt.gameObject.SetActive(false);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawRay(ray.origin, ray.direction * interactDistance);
    }
}
