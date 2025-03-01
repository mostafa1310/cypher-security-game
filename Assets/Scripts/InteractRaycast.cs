using UnityEngine;

public class InteractRaycast : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // assign the main camera in the inspector
    [SerializeField] private GameObject interactionPrompt; // assign a TMP Text UI element (e.g., "E" prompt)
    [SerializeField] private float interactDistance = 5f; // max raycast distance

    void Update()
    {
        if (InteractionManager.IsInteractionActive)
        {
            interactionPrompt.SetActive(false);
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            print(hit.collider.name);
            if (hit.collider.CompareTag("Interactable"))
            {
                // Show interaction prompt
                interactionPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Call the Interact method through IInteractable interface
                    hit.collider.GetComponent<IInteractable>()?.Interact();
                }
                return;
            }
        }
        // Hide interaction prompt if no interactable object is hit
        interactionPrompt.SetActive(false);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawRay(ray.origin, ray.direction * interactDistance);
    }
}
