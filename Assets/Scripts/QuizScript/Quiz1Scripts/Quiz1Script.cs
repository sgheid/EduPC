using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

//Classe che contiene i dati di gestione del primo livello del Quiz
public class Quiz1Script : MonoBehaviour{ //Fornisce funzioni base di Unity

    public List<QuestionAndAnswer> QnA; //Definisco la lista che contiene tutte le domande del primo livello del Quiz
    public GameObject[] options; //Definisco l'arrey che contiene tutte le opzioni ad ogni domanda
    private int CurrentQuestion; //Definisco l'indice della domanda
    public Text QuestionTxt; //Definisco il testo della domanda
    public GameObject QuizPanel; //Definisco il panel del primo livello del Quiz
    private int questionIndex = 0; //Definisco indice per regolarizzare il numero delle domande uscite
    private int score; //Definisco il punteggio del primo livello del Quiz
    public GameObject Lvl1; //Definisco il testo del primo livello del Quiz
    public GameObject Lvl2; //Definisco il testo del secondo livello del Quiz
    public Quiz2Script quiz2Script; //Definisco un oggetto di classe Quiz2Script per poterne richiamare i metodi

    //Metodo di avvio del primo livello del Quiz
    private void Start(){
        Invoke("GenerateQuestion", 1); //Ritardo di 1 secondo l'invocazione del metodo per generare la successiva domanda
    }

    //Metodo per gestire le risposte corrette
    public void Correct(){
        score += 1; //Aumento di uno il punteggio
        Debug.Log("Punteggio livello 1 aumentato: " + score); //Visualizza nel log l'attuale punteggio del primo livello del Quiz
        QnA.RemoveAt(CurrentQuestion); //Rimuovo l'attuale domanda dalla lista delle domande del primo livello del Quiz
        quiz2Script.DeleteQuestion(CurrentQuestion); //Richiamo il metodo DeleteQuestion dalla classe Quiz2Script, passando l'indice dell'attuale domanda
        Invoke("GenerateQuestion",0.5f); //Ritardo di 0.5 secondi l'invocazione del metodo per generare la successiva domanda
    }

    //Metodo per gestire le risposte sbagliate
    public void Wrong(){
        Debug.Log("Punteggio livello 1 invariato: " + score); //Visualizza nel log l'attuale punteggio del primo livello del Quiz
        QnA.RemoveAt(CurrentQuestion); //Rimuovo l'attuale domanda dalla lista delle domande del primo livello del Quiz
        quiz2Script.DeleteQuestion(CurrentQuestion); //Richiamo il metodo DeleteQuestion dalla classe Quiz2Script, passando l'indice dell'attuale domanda
        Invoke("GenerateQuestion",0.5f); //Ritardo di 0.5 secondi l'invocazione del metodo per generare la successiva domanda
    }

    //Metodo per impostare le risposte
    private void SetAnswers(){
        for(int i=0; i<options.Length; i++){ //Finchè non ho impostato ogni opzione
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().StartColor; //Reimposta il colore del pulsante a quello iniziale
            options[i].GetComponent<AnswerScript>().IsCorrect = false; //Assegna valore falso all'opzione
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[CurrentQuestion].Answers[i].ToUpper(); //Inserisce il testo della risposta nel pulsante e lo rende maiuscolo
            if(QnA[CurrentQuestion].correctAnswer == i+1){ //Confronta l'indice della risposta corretta con quello attuale
                options[i].GetComponent<AnswerScript>().IsCorrect = true; //Assegna valore vero all'opzione se è la risposta corretta
            }
        }
    }

    //Metodo per generare nuove domande
    private void GenerateQuestion(){
        if(questionIndex == 0){ //Se non sono ancora state visualizzate domande
            Lvl1.SetActive(false); //Disattivo la visualizzazione del testo del primo livello del Quiz
            QuizPanel.SetActive(true); //Attivo la visualizzazione del panel del primo livello del Quiz
        }
        if(questionIndex < 7){ //Limito il numero delle domande del primo livello del Quiz a 7
            CurrentQuestion = Random.Range(0, QnA.Count); //Assegna una domanda generica nella lista delle domande del primo livello del Quiz
            questionIndex++; //Incremento il numero di domande effettuate
            QuestionTxt.text = QnA[CurrentQuestion].Question; //Inserisce il testo della domanda
            SetAnswers(); //Richiamo il metodo per impostare le risposte
        }else{ //Se non ci sono piu' domande da generare
            Debug.Log("Out of Questions"); //Visualizza nel log che sono terminate le domande
            QuizPanel.SetActive(false); //Disattivo la visualizzazione del panel del primo livello del Quiz
            Lvl2.SetActive(true); //Attivo la visualizzazione del testo del secondo livello del Quiz
            Invoke("StartQuiz2", 1); //Ritardo di 1 secondo l'invocazione del metodo per avviare il secondo livello del Quiz
            //Start2(); //Avvia quiz2
        }
    }

    //Metodo per avviare il secondo livello del Quiz
    private void StartQuiz2(){
        quiz2Script.SetQuiz2(); //Richiamo il metodo SetQuiz2 dalla classe Quiz2Script
    }

    //Metodo per ottenere il punteggio del primo livello del Quiz
    public int GetScore(){
        return score; //Restituisce il punteggio del primo livello del Quiz
    }
}