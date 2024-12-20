using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuAssembla : MonoBehaviour
{
    public GameObject pauseMenuUI; // UI del menu di pausa
    public AudioMixer audioMixer; // Mixer audio principale
    public Slider volumeSlider;   // Slider per il volume
    private bool isMuted = false;  // Stato attuale di mute
    private float lastVolume = 1f; // Ultimo volume salvato prima di mutare
    private bool isPaused = false;

    void Start()
    {
        // Carica volume e mute
        LoadVolumeSettings();

        // Imposta lo slider volume
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void OnVolumeSliderChanged(float value)
    {
        // Se il valore dello slider è zero, attiva mute
        if (value == 0)
        {
            isMuted = true;
        }
        else
        {
            // Se il valore è sopra zero, disattiva mute e salva il volume
            isMuted = false;
            lastVolume = value;
        }

        ApplyVolume(); // Aggiorna il volume
    }
    private void ApplyVolume()
    {
        // Imposta il volume audio globale in base allo stato di mute
        AudioListener.volume = isMuted ? 0 : volumeSlider.value;
        SaveVolumeSettings();     // Salva le impostazioni
    }

    private void SaveVolumeSettings()
    {
        // Salva il volume e lo stato di mute
        PlayerPrefs.SetFloat("volume", lastVolume);
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        // Carica il volume e lo stato di mute dai PlayerPrefs
        lastVolume = PlayerPrefs.GetFloat("volume", 1f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        volumeSlider.value = isMuted ? 0 : lastVolume; // Imposta lo slider in base al mute
        ApplyVolume();                                 // Applica le impostazioni caricate
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void LoadModeSelectorMenu()
    {
        Time.timeScale = 1f; // Riprendi il tempo prima di cambiare scena
        SceneManager.LoadScene("ModeSelectorMenu");
    }
}
