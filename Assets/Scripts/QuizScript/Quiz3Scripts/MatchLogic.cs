using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

//Classe che contiene i dati di gestione dei punteggi del terzo livello del Quiz
public class MatchLogic : MonoBehaviour //Fornisce funzioni base di Unity
{
    private static MatchLogic Instance; //Definisco un oggetto di classe MatchLogic
    private int score = 0; //Definisco il punteggio del terzo livello del Quiz
    public QuizScoreScript quizScoreScript; //Definisco un oggetto di classe QuizScoreScript per richiamarne i metodi

    //Metodo di avvio di MatchLogic
    public void Start(){
        Instance = this; //Assegno questa istanza all'oggetto di classe MatchLogic
    }

    //Metodo per visualizzare i punteggi del Quiz
    public void VisualizzaPunteggi(){
        if(score == 5){ //Se il punteggio è uguale a 5
            quizScoreScript.GameOver(); //Richiamo il metodo GameOver della classe QuizScoreScript
        }
    }

    //Metodo per aggiornare il punteggio
    public static void UpdateScore(){
        AddPoints(1); //Richiamo il metodo per aggiungere punti al punteggio
    }

    //Metodo per aggiungere punti al punteggio passando come parametro i punti da aggiungere
    public static void AddPoints(int points){
        Instance.score += points; //Incremento il punteggio attuale dei punti da passati come attributo
        Debug.Log("Score: " + Instance.score); //Visualizzo nel log il punteggio attuale
        Instance.VisualizzaPunteggi(); //Richiamo il metodo per visualizzare i punteggi del Quiz
    }

    //Metodo per ottenere il punteggio del terzo livello del Quiz
    public int GetScore(){
        return score; //Restituisco il punteggio del terzo livello del Quiz
    }
}
