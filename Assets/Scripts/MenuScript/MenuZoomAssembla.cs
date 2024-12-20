using UnityEngine;
using UnityEngine.UI; // Aggiungi per la gestione della barra di caricamento

public class MenuZoomAssembla : MonoBehaviour
{
    public Transform zoomTarget; // Riferimento al target di zoom
    public float zoomSpeed = 2f;
    public Camera mainCamera;
    public GameObject canvas; // Riferimento al Canvas principale
    public GameObject loadingCanvas; // Riferimento al Canvas della barra di caricamento
    public Slider loadingBar; // Riferimento alla barra di caricamento (Slider UI)
    public float loadingTime = 2f; // Tempo di caricamento fittizio
    private Vector3 originalPosition;

    private bool isZooming = false;
    private bool zoomIn = false;
    private bool isLoading = false;
    private float currentLoadingTime = 0f;

    // Riferimento al tuo script di caricamento della scena
    public ModeSelectorScript sceneLoader; // Assicurati che il tuo script di caricamento scena sia assegnato qui

    void Start()
    {
        originalPosition = mainCamera.transform.position;

        // Assicurati che il canvas della barra di caricamento sia disabilitato all'inizio
        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }
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

                    // Avvia la barra di caricamento una volta completato lo zoom
                    if (loadingCanvas != null)
                    {
                        loadingCanvas.SetActive(true);
                        isLoading = true;
                        currentLoadingTime = 0f; // Resetta il tempo di caricamento
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

        // Gestisci il caricamento della barra
        if (isLoading)
        {
            currentLoadingTime += Time.deltaTime;

            // Aggiorna la barra di caricamento
            if (loadingBar != null)
            {
                loadingBar.value = currentLoadingTime / loadingTime;
            }

            // Quando il caricamento è completo, carica la scena
            if (currentLoadingTime >= loadingTime)
            {
                isLoading = false;

                if (loadingCanvas != null)
                {
                    loadingCanvas.SetActive(false); // Nascondi il canvas della barra di caricamento
                }

                // Chiamata per caricare la scena
                if (sceneLoader != null)
                {
                    sceneLoader.Assembla(); // Chiama il metodo di caricamento della scena
                }
            }
        }
    }

    public void ZoomIn()
    {
        // Disabilita il Canvas principale quando si fa clic sul bottone
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
