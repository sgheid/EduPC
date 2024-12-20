using UnityEngine;

public class TutorialPanelManager : MonoBehaviour
{
    public GameObject tutorialPanel;  // Il panel del tutorial
    private float tutorialDuration = 7f; // Durata del tutorial in secondi
    private float timer = 0f;  // Timer per il countdown
    private bool tutorialDisplayed = true; // Flag per sapere se il tutorial è già stato mostrato

    void Start()
    {
        // Assicuriamoci che il tutorial sia visibile all'inizio
        tutorialPanel.SetActive(true);
    }

    void Update()
    {
        // Se il tutorial è ancora visibile
        if (tutorialPanel.activeSelf && tutorialDisplayed)
        {
            timer += Time.deltaTime;

            // Dopo 7 secondi, nascondi il tutorial
            if (timer >= tutorialDuration)
            {
                tutorialPanel.SetActive(false);
                tutorialDisplayed = false;  // Non ripetere il timer
            }
        }
    }

    // Funzione per chiudere manualmente il tutorial premendo ESC
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialDisplayed = false; // Impedisce che il tutorial venga mostrato di nuovo
    }
}
