using UnityEngine;

public class MenuZoomOutMainMenu : MonoBehaviour
{
    public Transform zoomTarget; // Riferimento al target di zoom
    public float zoomSpeed = 2f;
    public Camera mainCamera;
    public GameObject canvas; // Riferimento al Canvas che vuoi disabilitare
    private Vector3 originalPosition;

    private bool isZooming = false;
    private bool zoomIn = false;

    // Riferimento al tuo script di caricamento della scena
    public ModeSelectorScript sceneLoader; // Assicurati che il tuo script di caricamento scena sia assegnato qui

    void Start()
    {
        originalPosition = mainCamera.transform.position;
    }

    void Update()
    {
        if (isZooming)
        {
            if (zoomIn)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, zoomTarget.position, Time.deltaTime * zoomSpeed);

                // Controlla se la camera è vicina al target
                if (Vector3.Distance(mainCamera.transform.position, zoomTarget.position) < 0.1f)
                {
                    isZooming = false;
                    // Chiamata per caricare la scena
                    if (sceneLoader != null)
                    {
                        sceneLoader.ReturnToMainMenu(); // Chiama il metodo di caricamento della scena
                    }
                }
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);

                // Controlla se la camera è tornata alla posizione originale
                if (Vector3.Distance(mainCamera.transform.position, originalPosition) < 0.1f)
                {
                    isZooming = false;
                }
            }
        }
    }

    public void ZoomIn()
    {
        // Disabilita il Canvas quando si fa clic sul bottone
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        isZooming = true;
        zoomIn = true;
    }

    public void ZoomOut()
    {
        isZooming = true;
        zoomIn = false;
    }
}