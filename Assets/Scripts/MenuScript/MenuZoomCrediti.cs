using UnityEngine;

public class MenuZoomCrediti : MonoBehaviour
{
    public Transform zoomTarget; // Riferimento al target di zoom
    public Quaternion zoomTargetRotation; // Rotazione target della camera
    public float zoomSpeed = 4f;
    public Camera mainCamera;
    public GameObject canvas; // Riferimento al Canvas che vuoi disabilitare
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isZooming = false;
    private bool zoomIn = false;

    // Riferimento al tuo script di caricamento della scena
    public MainMenu sceneLoader; // Assicurati che il tuo script di caricamento scena sia assegnato qui

    void Start()
    {
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation; // Salva la rotazione originale della camera
    }

    void Update()
    {
        if (isZooming)
        {
            if (zoomIn)
            {
                // Interpolazione della posizione
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, zoomTarget.position, Time.deltaTime * zoomSpeed);
                // Interpolazione della rotazione
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, zoomTargetRotation, Time.deltaTime * zoomSpeed);

                // Controlla se la camera è vicina al target
                if (Vector3.Distance(mainCamera.transform.position, zoomTarget.position) < 0.1f &&
                    Quaternion.Angle(mainCamera.transform.rotation, zoomTargetRotation) < 1f)
                {
                    isZooming = false;
                    // Chiamata per caricare la scena
                    if (sceneLoader != null)
                    {
                        sceneLoader.Crediti(); // Chiama il metodo di caricamento della scena
                    }
                }
            }
            else
            {
                // Interpolazione della posizione
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
                // Interpolazione della rotazione
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalRotation, Time.deltaTime * zoomSpeed);

                // Controlla se la camera è tornata alla posizione originale
                if (Vector3.Distance(mainCamera.transform.position, originalPosition) < 0.1f &&
                    Quaternion.Angle(mainCamera.transform.rotation, originalRotation) < 1f)
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
