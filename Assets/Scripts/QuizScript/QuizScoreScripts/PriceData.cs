using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

[System.Serializable] //Rende la classe serializzabile

//Classe che contiene i dati del messaggio e delle immagini da visualizzare una volta terminato il Quiz
public class PriceData{
    public string price; //Definisco messaggio da visualizzare una volta terminato il Quiz
    public Sprite emoticon; //Definisco sprite da visualizzare una volta terminato il Quiz
}