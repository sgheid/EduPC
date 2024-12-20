using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera; // Riferimento alla camera principale
    private bool isDragging = false; // Flag per controllare se stiamo trascinando l'oggetto
    private Vector3 offset; // Offset tra la posizione del mouse e l'oggetto
    private Rigidbody rb; // Riferimento al Rigidbody dell'oggetto
    private float initialY; // Posizione iniziale sull'asse Y
    private float liftHeight = 4f; // Altezza di sollevamento sull'asse Y

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y; // Salva la posizione iniziale sull'asse Y
    }

    private void OnMouseDown()
    {
        // Controlla se siamo nella modalità assemblaggio
        if (DeskInteraction.IsInAssemblyMode && !rb.isKinematic)
        {
            isDragging = true;
            rb.useGravity = false; // Disattiva la gravità per facilitare il trascinamento

            // Aggiungi l'offset per sollevare l'oggetto
            offset = transform.position - GetMouseWorldPosition();
            offset.y += liftHeight; // Solleva l'oggetto di 4 unità
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && !rb.isKinematic)
        {
            Vector3 newPos = GetMouseWorldPosition() + offset;
            newPos.y = initialY + liftHeight; // Mantieni l'altezza sollevata
            rb.MovePosition(newPos); // Muovi l'oggetto
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.useGravity = true; // Riattiva la gravità

        // Ripristina la posizione sull'asse Y all'altezza originale
        Vector3 finalPosition = transform.position;
        finalPosition.y = initialY;
        rb.MovePosition(finalPosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.WorldToScreenPoint(transform.position).z; // Mantieni la profondità
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }
}
