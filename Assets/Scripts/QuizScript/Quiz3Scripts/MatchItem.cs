using System.Collections; //Libreria che contiene interfacce e classi di varie collezioni di oggetti
using System.Collections.Generic; //Libreria che contiene interfacce e classi che definiscono raccolte generiche
using UnityEngine; //Libreria che permette di includere funzionalità basate su Unity
using UnityEngine.EventSystems; //Libreria che permette di inviare eventi semplici attraverso il sistema di eventi

//Classe che contiene i dati di gestione degli oggetti del terzo livello del Quiz

//MonoBehaviour: Fornisce funzioni base di Unity
//IPointerDownHandler: Interfaccia che permette di utilizzare il metodo OnPointerDown
//IDragHandler: Interfaccia che permette di utilizzare il metodo OnDrag
//IPointerEnterHandler: Interfaccia che permette di utilizzare il metodo OnPointerEnter
//IPointerUpHandler: Interfaccia che permette di utilizzare il metodo OnPointerUp
public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler{
    private static MatchItem hoverItem; //Definisco un oggetto di classe MachItem
    public GameObject linePrefab; //Definisco il prefab della linea da tracciare
    private GameObject line; //Definisco la linea tracciata
    public string itemName; //Definisco il nome dell'oggetto

    //Metodo per iniziare a tracciare una linea dall'oggetto su cui si clicca
    public void OnPointerDown(PointerEventData eventData){ //L'evento si attiva quando un puntatore diventa attivo, in questo caso quando si clicca con il mouse su un oggetto del terzo livello del Quiz
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent); //Clono il prefab della linea, modificandone posizione, orientamento e parent del nuovo oggetto assegnato alla linea
        line.SetActive(true);
        UpdateLine(eventData.position); //Richiamo il metodo per aggiornare la posizione della linea
    }

    //Metodo per tracciare una linea spostando il puntatore
    public void OnDrag(PointerEventData eventData){ //L'evento si attiva quando un puntatore viene spostato durante il trascinamento, in questo caso quando il mouse viene spostato
        UpdateLine(eventData.position); //Richiamo il metodo per aggiornare la posizione della linea
    }

    //Metodo per determinare il nuovo stato corrente 
    public void OnPointerUp(PointerEventData eventData){ //L'evento si attiva quando il puntatore diventa inattivo, in questo caso quando il mouse non viene più cliccato
        if(!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName)){ //Se questa istanza e l'oggetto MatchItem associato non sono uguali e hanno lo stesso nome
            UpdateLine(hoverItem.transform.position); //Aggiorno la posizione della linea
            MatchLogic.UpdateScore(); //Richiamo il metodo UpdateScore dalla classe MatchLogic
            Destroy(hoverItem); //Distruggo l'oggetto MatchItem associato
            Destroy(this); //Distruggo questa istanza
        }else{ //Se almeno una delle condizioni precedenti non è soddisfatta
            Destroy(line); //Distruggo la linea
        }
    }

    //Metodo per avviare un nuovo stato
    public void OnPointerEnter(PointerEventData eventData){ //L'evento si attiva dopo che è terminato il precedente evento
        hoverItem = this; //Assegno all'oggetto MatchItem l'attuale istanza
    }

    //Metodo per aggiornare la linea passandone la posizione come attributo
    public void UpdateLine(Vector3 position){
        Vector3 direction = position - transform.position; //Definisco la direzione della linea, assegnando la differenza tra l'attuale posizione e la posizione della linea spostata nello spazio 
        line.transform.right = direction; //Modifico la posizione della linea sull'asse x, assegnandone la direzione
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1); //Modifica la scala di trasformazione della linea, assegnando alle x il valore della direzione; lasciando invariate le y e z 
    }
}