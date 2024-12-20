using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragAndDropItem : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private float zDistanceToCamera;

    // Distanza target e margine di sicurezza
    public float targetDistance = 5.0f; // Distanza di avvicinamento
    public float rotationSpeed = 10.0f; // Velocità di rotazione

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        // Inizia a trascinare l'oggetto
        zDistanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        isDragging = true;

        // Disabilita la fisica
        rb.isKinematic = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Calcola la posizione target per avvicinare l'oggetto
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * targetDistance;

            // Sposta l'oggetto gradualmente verso la posizione target
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);

            // Gestisci la rotazione dell'oggetto con la rotella del mouse
            HandleRotation();
        }
    }

    void OnMouseUp()
    {
        // Rilascia l'oggetto
        isDragging = false;

        // Riattiva la fisica
        rb.isKinematic = false;
    }

    private void HandleRotation()
    {
        // Ottieni l'input della rotella del mouse
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0f)
        {
            // Ruota l'oggetto nello spazio in base all'input
            transform.Rotate(Vector3.up, scrollDelta * rotationSpeed * 100f, Space.World);
        }
    }

    void OnDrawGizmos()
    {
        if (isDragging)
        {
            // Mostra la posizione target con un gizmo per debug
            Gizmos.color = Color.red;
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * targetDistance;
            Gizmos.DrawWireSphere(targetPosition, 0.5f);
        }
    }
}
