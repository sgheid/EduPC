using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

//Classe che contiene i dati delle risposte del primo livello del Quiz
public class AnswerScript : MonoBehaviour{ //Fornisce funzioni base di Unity
    public bool IsCorrect = false; //Definisco variabile booleana per verificare se la risposta è corretta o sbagliata
    public Quiz1Script quiz1Script; //Definisco oggetto di classe Quiz1Script per richiamare i metodi
    public Color StartColor; //Definisco oggetto per salvare il colore iniziale del componente richiamato

    //Metodo per avviare AnswerScript
    private void Start(){
        StartColor = GetComponent<Image>().color; //Salvo il colore iniziale del pulsante in una variabile
    }

    //Metodo per gestire le risposte
    public void Answer(){
        if (IsCorrect){ //Se la risposta è corretta
            GetComponent<Image>().color = Color.green; //Assegna colore verde al pulsante
            Debug.Log("Correct Answer"); //Visualizzo nel log che la risposta è corretta
            quiz1Script.Correct(); //Richiamo il metodo Correct dalla classe Quiz1Script
        }
        else{ //Se la risposta è sbagliata
            GetComponent<Image>().color = Color.red; //Assegno colore rosso al pulsante
            Debug.Log("Wrong Answer"); //Visualizzo nel log che la risposta è sbagliata
            quiz1Script.Wrong(); //Richiamo il metodo Wrong dalla classe Quiz1Script
        }
    }
}
