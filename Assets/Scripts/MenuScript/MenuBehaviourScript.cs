using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public static string previousScene;
    public AudioMixer audioMixer;
    
    private void Start()
    {
        LoadPlayerPrefs();
        
    }

    void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }

    

    // Funzione per avviare il gioco
    public void Gioca()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("ModeSelectorMenu");
    }

    // Funzione per aprire il menu opzioni
    public void Opzioni()
    {
        previousScene = SceneManager.GetActiveScene().name;  // Salva la scena corrente
        SceneManager.LoadScene("OptionsMenu");
    }

    public void Crediti()
    {
        previousScene = SceneManager.GetActiveScene().name;  // Salva la scena corrente
        SceneManager.LoadScene("ScenaCrediti");
    }


    // Funzione per uscire dal gioco
    public void Esci()
    {
        Debug.Log("Esci dal gioco");
        Application.Quit();
    }
}
