using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity
using UnityEngine.SceneManagement; //Libreria che permette di gestire le scene in Unity
public class QuizScoreScript : MonoBehaviour{ //Fornisce funzioni base di Unity
    private int score1 = 0; //Definisco il punteggio del primo livello del Quiz
    public Text Score1Txt; //Definisco il testo del punteggio del primo livello del Quiz
    public Text Bonus1Txt; //Definisco il testo del bonus del primo livello del Quiz
    private bool Bonus1 = false; //Definisco variabile booleana di controllo sul bonus del primo livello del Quiz
    private int score2 = 0; //Definisco il punteggio del secondo livello del Quiz
    public Text Score2Txt; //Definisco il testo del punteggio del secondo livello del Quiz
    public Text Bonus2Txt; //Definisco il testo del bonus del secondo livello del Quiz
    private bool Bonus2 = false; //Definisco variabile booleana di controllo sul bonus del secondo livello del Quiz
    private int score3 = 0; //Definisco il punteggio del terzo livello del Quiz
    public Text Score3Txt; //Definisco il testo del punteggio del terzo livello del Quiz
    public Text Bonus3Txt; //Definisco il testo del bonus del terzo livello del Quiz
    private int scoreTot; //Definisco variabile booleana di controllo sul bonus del terzo livello del Quiz
    public Text scoreTotText; //Definisco testo del punteggio totale del Quiz
    public Text priceText; //Definisco messaggio da visualizzare al termine del Quiz
    public Image priceImage; //Definisco immagine da visualizzare al termine del Quiz
    public PriceData[] QnA3 = new PriceData[6]; //Definisco un array contenente testo del maessaggio e sprite dell'immagine da visualizzare al termine del Quiz
    public GameObject ImageQuizPanel; //Definisco il panel del terzo livello del Quiz
    public GameObject GoPanel; //Definisco il panel dei punteggi del Quiz
    public Quiz1Script quiz1Script; //Definisco un oggetto di classe Quiz1Script per richiamarne i metodi
    public Quiz2Script quiz2Script; //Definisco un oggetto di classe Quiz2Script per richiamarne i metodi
    public MatchLogic matchLogic; //Definisco un oggetto di classe MatchLogic per richiamarne i metodi

    //Metodo per la gestione del pulsante di riavvio del Quiz
    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Ricarica alla scena iniziale del quiz
    }

    //Metodo per gestire i punteggi del Quiz
    public void GameOver(){
        ImageQuizPanel.SetActive(false); //Disattivo la visualizzazione del panel del terzo livello del Quiz
        GoPanel.SetActive(true); //Attivo la visualizzazione del panel dei punteggi del Quiz
        score1 = quiz1Script.GetScore(); //Assegno il punteggio del primo livello del Quiz
        score2 = quiz2Script.GetScore(); //Assegno il punteggio del secondo livello del Quiz
        score3 = matchLogic.GetScore(); //Assegno il punteggio del terzo livello del Quiz
        Score1Txt.text = "Score: " + score1 + "/7"; //Inserisco il testo del punteggio del primo livello del Quiz
        if(score1 == 7){ //Se il punteggio del primo livello del Quiz è uguale a 7
            Bonus1 = true; //Attivo il bonus del primo livello del Quiz
            score1 ++; //Incremento il punteggio del primo livello del Quiz
            Bonus1Txt.text = "Bonus: 1"; //Inserisco il testo del bonus del primo livello del Quiz
        }
        Score2Txt.text = "Score: " + score2 + "/35"; //Inserisco il testo del punteggio del secondo livello del Quiz
        if(score2 == 35){ //Se il punteggio del secondo livello del Quiz è uguale a 7
            Bonus2 = true; //Attivo il bonus del secondo livello del Quiz
            score2 ++; //Incremento il punteggio del secondo livello del Quiz
            Bonus2Txt.text = "Bonus: 1"; //Inserisco il testo del bonus del secondo livello del Quiz
        }
        Score3Txt.text = "Score: " + score3 + "/5"; //Inserisco il testo del punteggio del terzo livello del Quiz
        if(Bonus1 && Bonus2){ //Se entrambi i bonus del primo e secondo livello del Quiz sono attivi
            score3 ++; //Incremento il punteggio del terzo livello del Quiz
            Bonus3Txt.text = "Bonus: 1"; //Inserisco il testo del bonus del terzo livello del Quiz
        }
        scoreTot = score1 + score2 + score3; //Asseegno il punteggio totale del Quiz
        scoreTotText.text = "Total score: " + scoreTot; //Inserisco il testo del punteggio totale del Quiz
        if(scoreTot == 5){ //Se il punteggio totale del Quiz è uguale a 5
            priceText.text = QnA3[0].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[0].emoticon; //Assegno la relativa immagine finale
        }
        else if(scoreTot > 5 && scoreTot <= 17){ //Se il punteggio totale del Quiz è maggiore di 5 e minore o uguale a 17
            priceText.text = QnA3[1].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[1].emoticon; //Assegno la relativa immagine finale
        }
        else if(scoreTot > 17 && scoreTot <= 30){ //Se il punteggio totale del Quiz è maggiore di 17 e minore o uguale a 30
            priceText.text = QnA3[2].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[2].emoticon; //Assegno la relativa immagine finale
        }
        else if(scoreTot > 30 && scoreTot <= 42){ //Se il punteggio totale del Quiz è maggiore di 30 e minore o uguale a 42
            priceText.text = QnA3[3].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[3].emoticon; //Assegno la relativa immagine finale
        }
        else if(scoreTot > 42 && scoreTot <= 49){ //Se il punteggio totale del Quiz è maggiore di 42 e minore o uguale a 49
            priceText.text = QnA3[4].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[4].emoticon; //Assegno la relativa immagine finale
        }
        else{ //Se il punteggio totale del Quiz è uguale a 50
            priceText.text = QnA3[5].price; //Assegno il relativo messaggio finale
            priceImage.sprite = QnA3[5].emoticon; //Assegno la relativa immagine finale
        }
    }
}