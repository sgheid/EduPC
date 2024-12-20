[System.Serializable] //Rendo la classe serializzabile

//Classe che contiene i dati delle domande e delle risposte del primo livello del Quiz
public class QuestionAndAnswer{
   public string Question; //Definisco la domanda
   public string[] Answers; //Definisco l'array delle risposte
   public int correctAnswer; //Definisco il numero delle risposte corrette
}