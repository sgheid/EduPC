using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;

    // Riferimenti alle varie tracce musicali
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip optionsMusic;
    public AudioClip modeMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mantieni l'oggetto tra le scene
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);  // Evita più istanze dell'AudioManager
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Ascolta quando cambia scena
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambia la musica in base alla scena
        switch (scene.name)
        {
            case "StartMenu":
                PlayMusic(menuMusic);
                break;
            case "ModeSelectorMenu":
                PlayMusic(modeMusic);
                break;
            case "ScenaAssemblatore":
                PlayMusic(gameplayMusic);
                break;
            case "OptionsMenu":
                PlayMusic(optionsMusic);
                break;
                // Aggiungi altri casi per altre scene
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)  // Cambia solo se la traccia è diversa
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}