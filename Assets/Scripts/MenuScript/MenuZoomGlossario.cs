using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuZoomGlossario : MonoBehaviour
{
    public Transform zoomTarget;
    public float zoomSpeed = 2f;
    public Camera mainCamera;
    public GameObject canvas;
    public GameObject loadingCanvas;
    public Slider loadingBar;
    public float loadingTime = 2f;
    private Vector3 originalPosition;

    private bool isZooming = false;
    private bool zoomIn = false;
    private bool isLoading = false;
    private float currentLoadingTime = 0f;
    private AsyncOperation asyncLoad;
    private float targetLoadingValue = 0f; // Valore verso cui interpolare la barra

    void Start()
    {
        originalPosition = mainCamera.transform.position;

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

                if (Vector3.Distance(mainCamera.transform.position, zoomTarget.position) < 0.1f)
                {
                    isZooming = false;

                    if (loadingCanvas != null)
                    {
                        loadingCanvas.SetActive(true);
                        isLoading = true;
                        currentLoadingTime = 0f;

                        asyncLoad = SceneManager.LoadSceneAsync("ScenaGlossario");
                        asyncLoad.allowSceneActivation = false;
                    }
                }
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);

                if (Vector3.Distance(mainCamera.transform.position, originalPosition) < 0.1f)
                {
                    isZooming = false;
                }
            }
        }

        if (isLoading)
        {
            currentLoadingTime += Time.deltaTime;

            if (asyncLoad != null)
            {
                targetLoadingValue = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Assegna il valore obiettivo
            }

            // Applica il lerp alla barra di caricamento per renderla fluida
            if (loadingBar != null)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, targetLoadingValue, Time.deltaTime * 5f); // Usa Time.deltaTime per un lerp fluido
            }

            if (targetLoadingValue >= 0.9f && currentLoadingTime >= loadingTime)
            {
                isLoading = false;

                if (loadingCanvas != null)
                {
                    loadingCanvas.SetActive(false);
                }

                asyncLoad.allowSceneActivation = true;
            }
        }
    }

    public void ZoomIn()
    {
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
