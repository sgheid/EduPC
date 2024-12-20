using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity

[System.Serializable] //Rende la classe serializzabile

//Classe che contiene i dati dei nomi e delle immagini dei componenti del terzo livello del Quiz
public class ImageData{
    public string ImageText; //Definisco il nome del componente
    public Sprite Image; //Definisco lo sprite del componente 
}
