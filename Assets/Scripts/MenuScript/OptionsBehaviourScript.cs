using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer MasterMixer;
    [SerializeField] private Slider VolumeSlider;
    public TMP_Dropdown ResolutionDropdown;
    private List<Resolution> uniqueResolutions;
    public Button ExitButton;
    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;

    private bool isMuted = false;
    private float lastVolume = 1f;

    void Start()
    {
        // Configura il dropdown delle risoluzioni
        ConfigureResolutionDropdown();

        // Carica volume e stato di mute
        LoadVolumeSettings();

        // Imposta lo slider volume
        VolumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);

        // Carica e imposta la risoluzione salvata
        LoadSavedResolution();

        // Collega il listener per cambiare risoluzione solo dopo l'aggiornamento iniziale
        ResolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void ConfigureResolutionDropdown()
    {
        Resolution[] allResolutions = Screen.resolutions;
        uniqueResolutions = new List<Resolution>();
        HashSet<string> addedResolutions = new HashSet<string>();
        List<string> options = new List<string>();

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string option = allResolutions[i].width + " x " + allResolutions[i].height;

            if (!addedResolutions.Contains(option))
            {
                uniqueResolutions.Add(allResolutions[i]);
                options.Add(option);
                addedResolutions.Add(option);
            }
        }

        ResolutionDropdown.ClearOptions();
        ResolutionDropdown.AddOptions(options);
    }

    private void LoadSavedResolution()
    {
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);

        if (savedResolutionIndex >= 0 && savedResolutionIndex < uniqueResolutions.Count)
        {
            // Applica la risoluzione salvata
            SetResolution(savedResolutionIndex);
        }
        else
        {
            // Nessuna risoluzione salvata, usa quella corrente
            UpdateDropdownToCurrentResolution();
        }
    }

    private void UpdateDropdownToCurrentResolution()
    {
        int currentResolutionIndex = 0;

        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            if (uniqueResolutions[i].width == Screen.currentResolution.width &&
                uniqueResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        // Disabilita temporaneamente il listener
        ResolutionDropdown.onValueChanged.RemoveListener(SetResolution);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
        ResolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < uniqueResolutions.Count)
        {
            Resolution selectedResolution = uniqueResolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

            // Salva la risoluzione scelta
            PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
            PlayerPrefs.Save();
        }
    }

    private void OnVolumeSliderChanged(float value)
    {
        if (value == 0)
        {
            isMuted = true;
        }
        else
        {
            isMuted = false;
            lastVolume = value;
        }

        ApplyVolume();
    }

    private void ApplyVolume()
    {
        AudioListener.volume = isMuted ? 0 : VolumeSlider.value;
        SaveVolumeSettings();
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("volume", lastVolume);
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        lastVolume = PlayerPrefs.GetFloat("volume", 1f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        VolumeSlider.value = isMuted ? 0 : lastVolume;
        ApplyVolume();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Ritorna()
    {
        SceneManager.LoadScene(MainMenu.previousScene);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitButton.onClick.Invoke();
        }
    }
}
