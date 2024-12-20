using UnityEngine;

public class SnapTrigger : MonoBehaviour
{
    public GameObject acceptedObject; // L'oggetto specifico accettato da questo trigger
    public Transform snapPosition;   // La posizione dove agganciare l'oggetto
    public AssemblyManager assemblyManager; // Riferimento all'AssemblyManager
    public float snapSpeed = 5f;     // Velocità dell'effetto di inserimento
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
            // Verifica prerequisiti nell'AssemblyManager
            if (assemblyManager != null)
            {
                bool canSnap = assemblyManager.CanInstallComponent(acceptedObject.name);
                if (!canSnap)
                {
                    Debug.LogWarning($"Non puoi installare {acceptedObject.name} ora! Verifica i prerequisiti.");
                    return;
                }
            }

            // Disabilita la fisica e il drag immediatamente
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            DragAndDrop dragScript = other.GetComponent<DragAndDrop>();
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

            // Aggiorna il check specifico nell'AssemblyManager
            if (assemblyManager != null)
            {
                assemblyManager.InstallComponent(acceptedObject.name);
                Debug.Log($"{acceptedObject.name} agganciato e segnalato all'AssemblyManager.");
            }
            else
            {
                Debug.LogWarning("AssemblyManager non assegnato nello script SnapTrigger!");
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
