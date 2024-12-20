using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentSelector : MonoBehaviour
{
    public RawImage displayArea; // Area per il render 3D
    public TextMeshProUGUI descriptionText; // Casella di testo per la descrizione
    public GameObject[] models; // Array di modelli 3D da mostrare
    public string[] descriptions; // Descrizioni per ogni componente

    private GameObject currentModel;

    public Camera renderCamera; // La camera che inquadra il modello 3D
    public float rotationSpeed = 10f; // Velocità di rotazione del modello
    public float cameraMoveSpeed = 10f; // Velocità di movimento della camera
    public float moveSpeed = 0.5f; // Velocità del movimento con WASD
    public float zoomSpeed = 2f; // Velocità di zoom
    public float minZoom = 2f; // Zoom minimo
    public float maxZoom = 10f; // Zoom massimo

    private Vector3 initialCameraPosition; // Posizione iniziale della camera
    private Quaternion initialModelRotation; // Rotazione iniziale del modello

    private void Start()
    {
        // Disattiva inizialmente tutti i modelli e la descrizione
        foreach (var model in models)
        {
            model.SetActive(false);
        }
        descriptionText.text = ""; // Pulisci il testo della descrizione

        // Salva la posizione e la rotazione iniziale della camera e del modello
        initialCameraPosition = renderCamera.transform.position;
    }

    // Metodo per gestire il click del pulsante
    public void OnComponentSelected(int index)
    {
        // Disattiva il modello corrente e ripristina la rotazione se c'è
        if (currentModel != null)
        {
            currentModel.transform.rotation = initialModelRotation; // Reset rotazione
            currentModel.SetActive(false);
        }

        // Nasconde la descrizione corrente
        descriptionText.text = "";

        // Mostra il nuovo modello
        currentModel = models[index];
        currentModel.SetActive(true);

        // Cambia la descrizione
        descriptionText.text = descriptions[index];

        // Resetta la posizione della camera e la rotazione iniziale del nuovo modello
        renderCamera.transform.position = initialCameraPosition;
        initialModelRotation = currentModel.transform.rotation;
    }

    private void Update()
    {
        // Controlla se il cursore è sopra l'area designata
        if (RectTransformUtility.RectangleContainsScreenPoint(displayArea.rectTransform, Input.mousePosition))
        {
            if (currentModel != null)
            {
                // Ruota il modello con il tasto sinistro del mouse o il tasto centrale
                if (Input.GetMouseButton(1) || Input.GetMouseButton(2))  // Tasto sinistro o centrale del mouse
                {
                    float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
                    float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

                    // Ruota il modello lungo gli assi X e Y
                    currentModel.transform.Rotate(Vector3.up, -rotationX, Space.World);
                    currentModel.transform.Rotate(Vector3.right, rotationY, Space.World);
                }

                // Movimento della camera con il tasto destro del mouse
                if (Input.GetMouseButton(0))  // Tasto destro del mouse
                {
                    float moveX = Input.GetAxis("Mouse X") * cameraMoveSpeed;
                    float moveY = Input.GetAxis("Mouse Y") * cameraMoveSpeed;

                    // Muovi la telecamera attorno al modello lungo X e Y
                    renderCamera.transform.Translate(-moveX, -moveY, 0);
                }

                // Zoom con la rotella del mouse
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    Vector3 cameraPosition = renderCamera.transform.position;
                    cameraPosition += renderCamera.transform.forward * scroll * zoomSpeed;

                    // Limita lo zoom tra minZoom e maxZoom
                    float distance = Vector3.Distance(cameraPosition, currentModel.transform.position);
                    if (distance > minZoom && distance < maxZoom)
                    {
                        renderCamera.transform.position = cameraPosition;
                    }
                }

                // Movimenti WASD per esplorare l'area 3D
                HandleWASDMovement();
            }
        }

        // Opzionale: tasto per resettare la visuale
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetView();
        }
    }

    // Gestione dei movimenti con WASD
    void HandleWASDMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            renderCamera.transform.position += renderCamera.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            renderCamera.transform.position -= renderCamera.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            renderCamera.transform.position -= renderCamera.transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            renderCamera.transform.position += renderCamera.transform.right * moveSpeed * Time.deltaTime;
        }
    }

    // Resetta la posizione della camera e la rotazione del modello
    void ResetView()
    {
        renderCamera.transform.position = initialCameraPosition;

        if (currentModel != null)
        {
            currentModel.transform.rotation = initialModelRotation;
        }
    }
}
