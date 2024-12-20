using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    public static bool IsInAssemblyMode { get; private set; } = false; // Variabile statica per il controllo della modalità
    public GameObject interactionText3D; // Oggetto del testo 3D
    public GameObject crosshair; // Riferimento al GameObject del Crosshair
    public Transform assemblyCameraPosition; // Posizione e rotazione della telecamera in modalità assemblaggio
    public Transform focusPoint; // Punto specifico che la telecamera deve inquadrare
    public PlayerMovementTutorial playerMovement; // Riferimento allo script PlayerMovement
    public PlayerCam playerCam; // Riferimento allo script PlayerCam
    public bool isAssemblyComplete = false; // Aggiungi questa variabile per segnare il completamento dell'assemblaggio

    private bool isNearDesk = false; // Verifica se il giocatore è vicino alla scrivania
    private Transform mainCamera; // La telecamera principale
    private Vector3 originalCameraPosition; // Posizione originale della telecamera
    private Quaternion originalCameraRotation; // Rotazione originale della telecamera

    void Start()
    {
        // Assicurati che il testo 3D sia inizialmente nascosto
        interactionText3D.SetActive(false);

        // Recupera la telecamera principale
        mainCamera = Camera.main.transform;

        // Assicurati che gli script siano assegnati
        if (playerMovement == null)
            Debug.LogError("PlayerMovementTutorial non assegnato nello script DeskInteraction.");

        if (playerCam == null)
            Debug.LogError("PlayerCam non assegnato nello script DeskInteraction.");

        // Assicurati che il Crosshair sia assegnato
        if (crosshair == null)
            Debug.LogError("Crosshair non assegnato nello script DeskInteraction.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isAssemblyComplete) // Aggiungi il controllo che l'assemblaggio non sia completato
        {
            isNearDesk = true;
            interactionText3D.SetActive(true); // Mostra il testo 3D
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDesk = false;
            interactionText3D.SetActive(false); // Nascondi il testo 3D
        }
    }

    void Update()
    {
        // Se il testo 3D è attivo, ruotalo per essere rivolto verso la telecamera
        if (interactionText3D.activeSelf)
        {
            interactionText3D.transform.LookAt(Camera.main.transform);
            interactionText3D.transform.Rotate(0, 180, 0); // Se il testo appare al contrario
        }

        // Controllo per entrare o uscire dalla modalità assemblaggio
        if (isNearDesk && !IsInAssemblyMode && Input.GetKeyDown(KeyCode.E))
        {
            EnterAssemblyMode();
        }
        else if (IsInAssemblyMode && Input.GetKeyDown(KeyCode.E))
        {
            ExitAssemblyMode();
        }
    }

    public void EnterAssemblyMode()
    {
        IsInAssemblyMode = true; // Imposta la modalità assemblaggio su attiva
        interactionText3D.SetActive(false);

        if (crosshair != null)
            crosshair.SetActive(false);

        originalCameraPosition = mainCamera.position;
        originalCameraRotation = mainCamera.rotation;

        mainCamera.position = assemblyCameraPosition.position;
        Vector3 directionToFocus = (focusPoint.position - mainCamera.position).normalized;
        mainCamera.rotation = Quaternion.LookRotation(directionToFocus);

        if (playerMovement != null)
            playerMovement.SetMovementState(false);

        if (playerCam != null)
        {
            playerCam.SetCameraControlState(false);
            playerCam.UnlockCursor();
        }

        Debug.Log("Modalità assemblaggio attivata");
    }

    public void ExitAssemblyMode()
    {
        IsInAssemblyMode = false; // Imposta la modalità assemblaggio su inattiva

        if (isNearDesk && !isAssemblyComplete) // Non ripristinare il testo se l'assemblaggio è completo
            interactionText3D.SetActive(true);

        if (crosshair != null)
            crosshair.SetActive(true);

        mainCamera.position = originalCameraPosition;
        mainCamera.rotation = originalCameraRotation;

        if (playerMovement != null)
            playerMovement.SetMovementState(true);

        if (playerCam != null)
        {
            playerCam.SetCameraControlState(true);
            playerCam.LockCursor();
        }

        Debug.Log("Modalità assemblaggio disattivata");
    }
}
