using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.UI; //Libreria che permette di gestire l'interfaccia grafica in Unity
using UnityEngine.SceneManagement; //Libreria che permette di gestire le scene in Unity

//Classe che contiene i dati di gestione del terzo livello del Quiz
public class Quiz3Script : MonoBehaviour //Fornisce le funzioni base di Unity
{
    public GameObject ImageQuizPanel; //Definisco il panel del terzo livello del Quiz
    public GameObject[] Image = new GameObject[14]; //Definisco un array contenente le immagini dei componenti
    public GameObject[] ImageText = new GameObject[14]; //Definisco un array contenente il nome dei componenti
    private List<int> indexTextList = new List<int>{0,6,3,12,8,1,11,5,2,13,10,7,9,4}; //Definisco una lista contenente gli indici dei nomi dei vari componenti
    private List <int> indexImageList = new List<int>{5,1,8,13,7,12,2,9,11,3,0,10,6,4}; //Definisco una lista contenente gli indici delle immagini dei vari componenti
    private int currentImageTxt; //Definisco l'indice dell'attuale nome del componente

    private int indexImage; //Definisco l'indice dell'attuale indice del componente
    public MatchLogic matchLogic; //Definisco un oggetto di classe MatchLogic
    public GameObject Lvl3; //Definisco il testo del terzo livello del Quiz

    //Metodo per generare le associazioni tra nome e immagine dei componenti
    public void GenerateImage(){
        Lvl3.SetActive(false); //Disattivo la visualizzazione del testo del terzo livello
        ImageQuizPanel.SetActive(true); //Attivo la visualizzazione del testo del terzo livello
        for(int i = 0; i < 14; i++){ //Per ogni componente
            Image[i].SetActive(false); //Disattivo la visualizzazione dell'immagine
            ImageText[i].SetActive(false); //Disattivo la visualizzazione del nome
        }
        for(int i = 0; i < 5; i++){ //Per ognuno dei 5 componenti selezionati
            indexImage = Random.Range(0,indexTextList.Count); //Assegno un indice casuale tra gli indici della lista dei nomi dei componenti
            while(indexTextList[indexImage] == -1){ //Finchè l'indice attuale è uguale a -1
                indexImage = Random.Range(0,indexTextList.Count); //Assegno un nuovo indice casuale tra gli indici della lista dei nomi dei componenti
            }
            currentImageTxt = indexTextList[indexImage]; //Assegno l'attuale indice del nome del componente
            indexTextList[indexImage] = -1; //Assegno -1 per segnalare che è un componente già selezionato
            ImageText[indexImage].SetActive(true); //Attivo la visualizzazione del nome del componente
            for(int j = 0; j < indexImageList.Count; j++){ //Per ogni immagine del componente
                if(indexImageList[j] == currentImageTxt){ //Se l'indice dell'immagine e del nome del componente sono uguali
                    Image[j].SetActive(true); //Attivo la visualizzazione dell'immagine del componente
                }
            }
        }
    }
}
