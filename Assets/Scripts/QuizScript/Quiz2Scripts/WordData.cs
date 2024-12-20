using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

//Classe che contiene i dati delle lettere da inserire nel secondo livello del Quiz
public class WordData : MonoBehaviour{ //Fornisce funzioni base di Unity
    
    [SerializeField] //Forza Unity a serializzare una variabile privata
    private Text charText; //Definisco il testo del pulsante

    [HideInInspector] //Impedisco la visualizzazione di una variabile nell'inspector
    public char charValue; //Definisco il carattere da inserire
    private Button buttonObj; //Definisco i pulsanti per selezionare le lettere da inserire

    //Metodo richiamato prima dell'avvio di WordData per inizializzare i pulsanti
    private void Awake(){
        buttonObj = GetComponent<Button>(); //Assegno un riferimento al pulsante
        if(buttonObj){ //Se il pulsante esiste
            buttonObj.onClick.AddListener(() => CharSelected()); //Assegno la funzione CharSelected() al pulsante quando cliccato
        }
    }

    //Metodo per impostare i caratteri
    public void SetChar(char value){
        charText.text = value + ""; //Inserisco la lettera nel testo del pulsante
        charValue = value; //Inizializzo il carattere da inserire
    }

    //Metodo per Selezionare un carattere
    private void CharSelected(){
        Quiz2Script.istance.SelectedOption(this); //Assegno ad istance l'istanza del carattere selezionato
    }
}