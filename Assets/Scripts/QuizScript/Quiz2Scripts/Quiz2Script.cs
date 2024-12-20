using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity
using System.Linq; //Libreria che fornisce classi e interfacce che supportano query basate su LINQ

//Classe che contiene i dati di gestione del secondo livello del Quiz
public class Quiz2Script : MonoBehaviour //Fornisce funzioni base di Unity
{
    public GameObject WordQuizPanel; //Definisco il panel del secondo livello del Quiz
    public static Quiz2Script istance; //Definisco un'istanza per poter uscire da Quiz2Script senza passare reference
    public List<QuestionData> QnA2; //Definisco la lista che contiene tutte le domande del secondo livello del Quiz
    [SerializeField] //Forza Unity a serializzare una variabile privata
    private WordData[] answerWordArray; //Definisco un array contenente le celle in cui inserire la risposta
    [SerializeField] //Forza Unity a serializzare una variabile privata
    private WordData[] optionsWordArray; //Definisco un array contenente le celle delle opzioni
    private char[] charArray = new char[16]; //Definisco un array contenente le lettere della risposta
    private int currentAnswerIndex = 0; //Definisco l'indice dell'ultima lettera della risposta inserita
    private bool correctAnswer = true; //Definisco una variabile booleana per verificare se la risposta inserita è corretta
    private List<int> selectedWordIndex; //Definisco una lista contenente l'indice delle lettere selezionate
    public Text questionTxt2; //Definisco il testo della domanda
    private string answerWord; //Definisco la risposta
    private int noSpaceIndex = 0; //Definisco l'indice delle celle senza spazi
    private char[] tempCharArray = new char[16]; //Definisco un array di supporto per il salvataggio della risposta senza spazi
    private int decScore = 0; //Definisco il decrementatore del punteggio
    private int score; //Definisco il punteggio del secondo livello del Quiz
    private int CurrentQuestion; //Definisco l'indice della domanda
    private int questionIndex = 0; //Definisco indice per regolarizzare il numero delle domande uscite
    public GameObject Lvl2; //Definisco il testo del secondo livello del Quiz
    public GameObject Lvl3; //Definisco il testo del terzo livello del Quiz
    public GameObject CorrectAnswerPanel; //Definisco il panel della risposta corretta
    public Text correctAnswerText; //Definisco il testo della risposta corretta
    public Quiz3Script quiz3Script; //Definisco un oggetto di classe Quiz3Script per poterne richiamare i metodi

    //Metodo richiamato all'avvio di Quiz2Script per inizializzare le istanze e la lista degli indici delle lettere selezionate
    private void Awake(){
        if(istance == null){ //Se l'istanza non esiste
            istance = this; //Inizializzo Quiz2Script come l'attuale istanza
        }
        else{ //Se l'istanza esiste già
            Destroy(gameObject); //Distruggo i gameObject preesistenti
        }
        selectedWordIndex = new List<int>(); //Inizializzo la lista degli indici delle lettere selezionate
    }

    //Metodo che inizializza il secondo livello del Quiz
    public void SetQuiz2(){
        Lvl2.SetActive(false); //Disattivo la visualizzazione del testo del secondo livello del Quiz
        WordQuizPanel.SetActive(true); //Attivo la visualizzazione del panel del secondo livello del Quiz
        SetQuestion2(); //Richiamo il metodo per generare la successiva domanda
    }

    //Metodo per generare le domande del quiz2
    private void SetQuestion2(){
        decScore = 0; //Resetto il decrementatore del punteggio
        noSpaceIndex = 0; //Resetto l'indice delle lettere senza spazi
        currentAnswerIndex = 0; //Resetto l'indice dell'attale domanda
        selectedWordIndex.Clear(); //Resetto la lista degli indici delle lettere selezionate
        CurrentQuestion = Random.Range(0, QnA2.Count); //Assegno una domanda generica nella lista delle domande del secondo livello del Quiz
        questionIndex++; //Incremento l'indice delle domande uscite
        questionTxt2.text = QnA2[CurrentQuestion].question; //Assegno il testo della domanda
        answerWord = QnA2[CurrentQuestion].answer; //Assegno il testo della risposta
        ResetQuestion(); //Richiamo il medoto per resettare la domanda
        for(int i = 0; i < answerWord.Length; i++){ //Per ogni elemento della risposta
            tempCharArray[i] = char.ToUpper(answerWord[i]); //Rendo maiuscolo il carattere e lo assegna all'array di supporto della risposta
            if(tempCharArray[i] == 32){ //Se il carattere salvato è uno spazio bianco
                answerWordArray[i].SetChar(' '); //Imposto testo vuoto nella cella della risposta
                answerWordArray[i].GetComponent<Image>().enabled = false; //Disabilito lo sfondo della cella della risposta
            }
            else{ //Se il carattere non è uno spazio bianco
                charArray[noSpaceIndex] = tempCharArray[i]; //Assegno il carattere all'array delle opzioni
                noSpaceIndex++; //Incremento l'indice dei caratteri senza spazi
            }
        }  
        for(int i = noSpaceIndex; i < optionsWordArray.Length; i++){ //Per ogni elemento dell'array delle opzioni, a partire dall'indice della risposta senza spazi
            charArray[i] = (char)UnityEngine.Random.Range(65, 91); //Assegno un carattere a caso generato dalla A alla Z
        }
        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray(); //Rimescolo casualmente gli elementi nell'array della risposta
        for(int i = 0; i < optionsWordArray.Length; i++){ //Per ogni elemento dell'array delle opzioni
            optionsWordArray[i].SetChar(charArray[i]); //Imposto il testo in ogni cella delle opzioni
        }
    }

    //Metodo per selezionare le lettere opzionabili
    public void SelectedOption(WordData wordData){
        if(currentAnswerIndex >= answerWord.Length){ //Se sono state inserite tutte le lettere della risposta
            return; //termino il metodo
        }
        selectedWordIndex.Add(wordData.transform.GetSiblingIndex()); //Assegno l'indice della lettera selezionata
        if(!answerWordArray[currentAnswerIndex].GetComponent<Image>().enabled){ //Se la cella in cui inserire la lettera non ha lo sfondo
            currentAnswerIndex++; //Incremento l'indice dell'ultima lettera inserita
        }
        answerWordArray[currentAnswerIndex].SetChar(wordData.charValue); //Inserisco il carattere selezionato nella cella della risposta
        wordData.gameObject.SetActive(false); //Disabilito la cella delle opzioni selezionata
        currentAnswerIndex++; //Incremento l'indice dell'ultima lettera inserita
        if(currentAnswerIndex >= answerWord.Length){ //Se ho inserito tutte le lettere della risposta
            correctAnswer = true; //Assegno vero alla variabile booleana
            for(int i = 0; i < answerWord.Length; i++){ //Per ogni elemento della risposta
                if(char.ToUpper(answerWord[i]) != char.ToUpper(answerWordArray[i].charValue)){ //Rendo ogni carattere maiuscolo e se il carattere della risposta è diverso da quello inserito
                    correctAnswer = false; //Assegno falso alla variabile booleana
                    break; //Interrompo il ciclo for
                }
            }
            if(correctAnswer){ //Se la risposta è corretta
                DeleteQuestion(CurrentQuestion); //Richiamo il metodo per rimuovere l'attuale domanda dalla lista delle domande del secondo livello del Quiz
                score = score + 5 - decScore; //Incremento il punteggio di 5, se non ho commesso errori, altrimenti lo incremento della differenza tra 5 e il numero di errori
                Debug.Log("Correct answer: " + score); //Visualizzo nel log che la risposta è corretta con il punteggio attuale
                if(questionIndex < 7){ //Limito il numero delle domande del secondo livello del Quiz a 7
                    Invoke("SetQuestion2", 0.5f); //Ritardo di 0.5 secondi l'invocazione del metodo per generare la successiva domanda
                }
                else{ //Se ho terminato le domande
                    WordQuizPanel.SetActive(false); //Disattivo la visualizzazione del panel del secondo livello del Quiz
                    Lvl3.SetActive(true); //Attivo la visualizzazione del terzo livello del Quiz
                    Invoke("StartQuiz3", 1); //Ritardo di 1 secondo l'invocazione del metodo per avviare il terzo livello del Quiz
                    //GameOver(); //Richiamo il metodo per gestire la fine del quiz
                }
            }
            else{ //Se la risposta è sbagliata
                Debug.Log("Wrong answer"); //Visualizzo nel log che la risposta è sbagliata
                if(decScore < 5){ //Se il decrementatore del punteggio è minore di 5
                    decScore ++; //Incremento il decrementatore del punteggio
                }
                else{ //Se il decrementatore del punteggio è maggiore o uguale a 5
                    WordQuizPanel.SetActive(false); //Disattivo la visualizzazione del panel del secondo livello del Quiz 
                    CorrectAnswerPanel.SetActive(true); //Attivo la visualizzazione del panel della risposta corretta
                    correctAnswerText.text = QnA2[CurrentQuestion].answer.ToUpper(); //Assegno il testo della risposta corretta, rendendo le lettere tutte maiuscole
                    DeleteQuestion(CurrentQuestion); //Richiamo il metodo per rimuovere l'attuale domanda dalla lista delle domande del secondo livello del Quiz
                    Debug.Log("Terminati tentativi"); //Visualizzo nel log che i tentativi per rispondere correttamente sono terminati
                    Debug.Log("Score: " + score); //Visualizzo nel log l'attuale punteggio
                    Invoke("DelayPanelReset", 1); //Ritardo di 1 secondo l'invocazione del metodo per ritardare il cambio panel
                }
            }
        }
    }
    
    //Metodo per resettare le domande
    private void ResetQuestion(){
        for(int i = 0; i < answerWordArray.Length; i++){ //Per ogni elemento dell'array delle celle della risposta
            answerWordArray[i].gameObject.SetActive(true); //Attivo la visualizzazione di ogni cella
            answerWordArray[i].GetComponent<Image>().enabled = true; //Attivo lo sfondo di ogni cella
            answerWordArray[i].SetChar('_'); //Assegno il trattino basso ad ogni cella
        }
        for(int i = answerWord.Length; i < answerWordArray.Length; i++){ //Per ogni elemento dell'array delle celle che non conterrà lettere
            answerWordArray[i].gameObject.SetActive(false); //Disattivo la visualizzazione di ogni cella
        }
        for(int i = 0; i < optionsWordArray.Length; i++){ //Per ogni elemento dell'array delle celle selezionabili
            optionsWordArray[i].gameObject.SetActive(true); //Attivo la visualizzazione di ogni cella
        }
    }

    //Metodo per cancellare l'ultima lettera inserita
    public void ResetLastWord(){
        if(selectedWordIndex.Count > 0){ //Se ci sono lettere selezionate
            if(!answerWordArray[currentAnswerIndex - 1].GetComponent<Image>().enabled){ //Se lo sfondo della penultima cella è disattivato
                currentAnswerIndex--; //Decremento l'indice dell'ultima lettera inserita
            }
            int index = selectedWordIndex[selectedWordIndex.Count - 1]; //Definisco una variabile di supporto per salvare l'indice dell'ultima lettera selezionata
            optionsWordArray[index].gameObject.SetActive(true); //Attivo la visualizzazione della cella opzionabile dell'ultima lettera selezionata
            selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1); //Rimuovo l'ultima lettera inserita
            currentAnswerIndex--; //Decremento l'indice dell'ultima lettera inserita
            answerWordArray[currentAnswerIndex].SetChar('_'); //Inserisco il trattino basso nella cella dell'ultima lettera inserita
        }
    }

    //Metodo per resettare la risposta inserita
    public void ResetAll(){
        while(selectedWordIndex.Count > 0){ //Finchè ho lettere selezionate
            if(!answerWordArray[currentAnswerIndex - 1].GetComponent<Image>().enabled){ //Se lo sfondo della penultima cella è disattivato
                currentAnswerIndex--; //Decrementa l'indice dell'ultima lettera inserita
            }
            int index = selectedWordIndex[selectedWordIndex.Count - 1]; //Definisco una variabile di supporto per salvare l'indice dell'ultima lettera selezionata
            optionsWordArray[index].gameObject.SetActive(true); //Attivo la visualizzazione della cella opzionabile dell'ultima lettera selezionata
            selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1); //Rimuovo l'ultima lettera inserita
            currentAnswerIndex--; //Decremento l'indice dell'ultima lettera inserita
            answerWordArray[currentAnswerIndex].SetChar('_'); //Inserisco il trattino basso nella cella dell'ultima lettera inserita
        }
    }

    //Metodo per eliminare l'attuale domanda dalla lista delle domande del secondo livello del Quiz passandone l'indice come attributo
    public void DeleteQuestion(int index){
        QnA2.RemoveAt(index); //Rimuovo l'attuale domanda dalla lista delle domande del secondo livello del Quiz
    }

    //Metodo per ritardare il cambio dal panel della risposta corretta ad un altro
    private void DelayPanelReset(){
        CorrectAnswerPanel.SetActive(false); //Disattivo la visualizzazione del panel della risposta corretta
        if(questionIndex < 7){ //Se l'indice delle domande uscite è minore di 7
            WordQuizPanel.SetActive(true); //Attivo la visualizzazione del panel del secondo livello del quiz
            SetQuestion2(); //Richiamo il metodo per generare la successiva domanda
        }
        else{ //Se l'indice delle domande uscite è maggiore o uguale a 7
            Lvl3.SetActive(true); //Attivo la visualizzazione del testo del terzo livello del Quiz
            Invoke("StartQuiz3", 1); //Ritardo di 1 secondo l'invocazione del metodo per avviare il terzo livello del Quiz
        }
    }

    //Metodo per avviare il terzo livello del Quiz
    private void StartQuiz3(){
        Lvl3.SetActive(false); //Disattivo la visualizzazione del terzo livello del Quiz
        quiz3Script.GenerateImage(); //Richiamo il metodo GenerateImage dalla classe Quiz3Script
    }

    //Metodo per ottenere il punteggio del secondo livello del Quiz
    public int GetScore(){
        return score; //Restituisce il punteggio del secondo livello del Quiz
    }
}