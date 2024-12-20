
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsFade : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Assegna il CanvasGroup dal tuo Canvas
    public float fadeDuration = 2f; // Durata della dissolvenza
    public float displayTime = 3f;  // Tempo di visualizzazione

    private IEnumerator Start()
    {
        // Assicuriamoci che il CanvasGroup sia invisibile all'inizio
        canvasGroup.alpha = 0;

        // Fai comparire la scritta con dissolvenza
        yield return StartCoroutine(FadeIn());

        // Aspetta il tempo di visualizzazione
        yield return new WaitForSeconds(displayTime);

        // Fai scomparire la scritta con dissolvenza
        yield return StartCoroutine(FadeOut());

        // Carica la scena successiva
        LoadNextScene();
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1; // Assicura che sia completamente visibile
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1 - (timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0; // Assicura che sia completamente invisibile
    }

    private void LoadNextScene()
    {
        // Sostituisci "NextScene" con il nome della tua prossima scena
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
