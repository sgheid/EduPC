using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel; // Finestra informativa
    public Button backButton; // Bottone "Indietro" per chiudere la finestra
    public CanvasGroup backgroundCanvasGroup; // CanvasGroup del canvas di sfondo

    private void Start()
    {
        // Assicura che la finestra informativa sia inizialmente nascosta
        

        // Collega il bottone "Indietro" alla funzione di chiusura
        backButton.onClick.AddListener(CloseInfoPanel);
    }

    // Metodo per aprire la finestra informativa
    public void ShowInfo()
    {
        // Attiva solo se il pannello è chiuso
        if (!infoPanel.activeSelf)
        {
            infoPanel.SetActive(true); // Mostra la finestra
            if (backgroundCanvasGroup != null)
            {
                backgroundCanvasGroup.interactable = false; // Disabilita l’interazione del canvas di sfondo
                backgroundCanvasGroup.blocksRaycasts = false; // Impedisce la ricezione dei clic
            }
        }
    }

    // Metodo per chiudere la finestra informativa
    private void CloseInfoPanel()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false); // Nascondi la finestra
            if (backgroundCanvasGroup != null)
            {
                backgroundCanvasGroup.interactable = true; // Riabilita l’interazione del canvas di sfondo
                backgroundCanvasGroup.blocksRaycasts = true; // Permette di nuovo la ricezione dei clic
            }
        }
    }
}
