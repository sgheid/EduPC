using UnityEngine;

public class SnapTriggerCase : MonoBehaviour
{
    [Header("Oggetto accettato e posizione di snap")]
    public GameObject acceptedObject; // L'oggetto specifico accettato da questo trigger
    public Transform snapPosition;   // La posizione dove agganciare l'oggetto

    [Header("Impostazioni di snapping")]
    public float snapSpeed = 5f;     // Velocità dell'effetto di inserimento

    [Header("Gestione completamento")]
    public AssemblyManagerCase assemblyManager; // Riferimento all'AssemblyManagerCase per aggiornare i progressi

    [Header("Impostazioni audio")]
    public AudioClip snapSound;      // Suono da riprodurre al momento dello snap
    private AudioSource audioSource; // Componente AudioSource per il suono

    private bool isSnapped = false;  // Flag per verificare se l'oggetto è già stato posizionato

    private void Start()
    {
        // Aggiungi un AudioSource al GameObject se non è già presente
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == acceptedObject && !isSnapped)
        {
            // Verifica i prerequisiti nell'AssemblyManagerCase
            if (assemblyManager != null)
            {
                bool canSnap = assemblyManager.CanInstallComponent(acceptedObject.name);
                if (!canSnap)
                {
                    Debug.LogWarning($"Non puoi installare {acceptedObject.name} ora! Verifica i prerequisiti.");
                    return;
                }
            }

            Debug.Log($"{other.gameObject.name} rilevato dal trigger.");

            // Disabilita la fisica dell'oggetto
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Disabilita lo script DragAndDropItem per impedire ulteriori spostamenti
            DragAndDropItem dragScript = other.GetComponent<DragAndDropItem>();
            if (dragScript != null)
            {
                Destroy(dragScript); // Rimuovi lo script di drag & drop
            }

            // Inizia il movimento verso la posizione di snap
            StartCoroutine(SnapObject(other.transform));

            // Riproduci il suono dello snap
            if (snapSound != null)
            {
                audioSource.PlayOneShot(snapSound);
            }

            // Segna l'oggetto come agganciato
            isSnapped = true;

            // Aggiorna il contatore dei check nell'AssemblyManagerCase
            if (assemblyManager != null)
            {
                assemblyManager.InstallComponent(acceptedObject.name);
                Debug.Log("Check completato inviato all'AssemblyManagerCase.");
            }
            else
            {
                Debug.LogWarning("AssemblyManagerCase non assegnato nello script SnapTriggerCase!");
            }
        }
    }

    private System.Collections.IEnumerator SnapObject(Transform objectTransform)
    {
        float progress = 0f;
        Vector3 initialPosition = objectTransform.position;
        Quaternion initialRotation = objectTransform.rotation;

        while (progress < 1f)
        {
            progress += Time.deltaTime * snapSpeed;

            // Interpolazione lineare della posizione e rotazione
            objectTransform.position = Vector3.Lerp(initialPosition, snapPosition.position, progress);
            objectTransform.rotation = Quaternion.Lerp(initialRotation, snapPosition.rotation, progress);

            yield return null; // Aspetta il frame successivo
        }

        // Assicura che l'oggetto sia esattamente nella posizione e rotazione finale
        objectTransform.position = snapPosition.position;
        objectTransform.rotation = snapPosition.rotation;

        Debug.Log($"{objectTransform.gameObject.name} agganciato con effetto di inserimento!");
    }
}
