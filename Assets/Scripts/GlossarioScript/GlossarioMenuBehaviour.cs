using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlossarioMenuBehaviour : MonoBehaviour


{
    private static string previousScene;
    public Button ExitButton;
    public static void SetPreviousScene(string sceneName)
    {
        previousScene = sceneName;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("ModeSelectorMenu");
    }

    public void Opzioni()
    {
        MainMenu.previousScene = SceneManager.GetActiveScene().name;  // Salva la scena corrente
        SceneManager.LoadScene("OptionsMenu");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitButton.onClick.Invoke();
        }
    }
}