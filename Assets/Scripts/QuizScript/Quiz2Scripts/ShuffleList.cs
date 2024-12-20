using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity

//Classe astratta che contiene i dati per mescolare le lettere opzionabili del secondo livello del Quiz
public abstract class ShuffleList{

    //Metodo per mescolare le lettere della risposta del secondo livello del Quiz
    public static List<E> ShuffleListItems<E>(List<E> inputList){
        List<E> originalList = new List<E>(); //Definisco una lista 
        originalList.AddRange(inputList);  //Inserisco gli elementi della lista passata come attributo del metodo nella nuova lista
        List<E> randomList = new List<E>(); //Definisco una lista randomica

        System.Random r = new System.Random(); //Definisco una variabile randomica
        int randomIndex = 0; //Definisco indice lista randomica
        while(originalList.Count > 0){ //Finchè ci sono elementi nella lista
            randomIndex = r.Next(0, originalList.Count); //Scelgo randomicamente un oggetto nella lista
            randomList.Add(originalList[randomIndex]); //Aggiungo il nuovo oggetto alla lista randomica
            originalList.RemoveAt(randomIndex); //Rimuovo l'oggetto dalla lista originale per evitare duplicati
        }
        return randomList; //Restituisco una lista randomica
    }
}

