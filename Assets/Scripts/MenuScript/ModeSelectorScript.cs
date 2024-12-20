using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectorScript : MonoBehaviour
{
    public static string previousScene;
    public Button ReturnButton;

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

    public void Assembla()
    {
        SceneManager.LoadScene("ScenaAssemblatore");
    }

    public void Quiz()
    {
        SceneManager.LoadScene("ScenaQuiz");
    }

    public void Glossario()
    {
        MainMenu.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("ScenaGlossario");
    }

    public void Opzioni()
    {
        MainMenu.previousScene = SceneManager.GetActiveScene().name;  // Salva la scena corrente
        SceneManager.LoadScene("OptionsMenu");
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnButton.onClick.Invoke();
        }
    }


}
