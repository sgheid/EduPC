using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsMenuBehaviour : MonoBehaviour
{

    public Button ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ritorna()
    {
        SceneManager.LoadScene(MainMenu.previousScene); // Torna alla scena precedente
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitButton.onClick.Invoke();
        }
    }
}
