using UnityEngine;
using TMPro;

public class AssemblyTextUpdater : MonoBehaviour
{
    [Header("Riferimenti agli oggetti di testo")]
    public TextMeshPro zeroCheckText;
    public TextMeshPro firstCheckText;    // Testo per il primo check (Scheda madre assemblata)
    public TextMeshPro secondCheckText;   // Testo per il secondo check (Scheda madre montata sul case)
    public TextMeshPro thirdCheckText;    // Testo per il terzo check (GPU montata)
    public TextMeshPro fourthCheckText;   // Testo per il quarto check (HDD montato)
    public TextMeshPro fifthCheckText;    // Testo per il quinto check (Alimentatore montato)
    public TextMeshPro sixthCheckText;    // Testo per il sesto check (Pannello montato)

    [Header("Riferimenti al gestore dell'assemblaggio")]
    public AssemblyManagerCase assemblyManager; // Riferimento al manager dell'assemblaggio
    public AssemblyManager assemblyManager2;
    private void Start()
    {
        // Impostazione iniziale dei testi, nascondi tutti tranne il primo
        zeroCheckText.gameObject.SetActive(true);
        firstCheckText.gameObject.SetActive(false);
        secondCheckText.gameObject.SetActive(false);
        thirdCheckText.gameObject.SetActive(false);
        fourthCheckText.gameObject.SetActive(false);
        fifthCheckText.gameObject.SetActive(false);
        sixthCheckText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Aggiorna i testi in base ai check completati
        if (assemblyManager != null)
        {
            // Verifica se il primo check (scheda madre assemblata) è completato
            if (assemblyManager2.IsComponentInstalled("CpuRaff"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(true);
                secondCheckText.gameObject.SetActive(false);
                thirdCheckText.gameObject.SetActive(false);
                fourthCheckText.gameObject.SetActive(false);
                fifthCheckText.gameObject.SetActive(false);
                sixthCheckText.gameObject.SetActive(false);
            }

            // Verifica se il secondo check (scheda madre montata sul case) è completato
            if (assemblyManager.IsComponentInstalled("MBSpostabile"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(false);
                secondCheckText.gameObject.SetActive(true);
                thirdCheckText.gameObject.SetActive(false);
                fourthCheckText.gameObject.SetActive(false);
                fifthCheckText.gameObject.SetActive(false);
                sixthCheckText.gameObject.SetActive(false);
            }

            // Verifica se il terzo check (GPU montata) è completato
            if (assemblyManager.IsComponentInstalled("GPUSpostabile"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(false);
                secondCheckText.gameObject.SetActive(false);
                thirdCheckText.gameObject.SetActive(true);
                fourthCheckText.gameObject.SetActive(false);
                fifthCheckText.gameObject.SetActive(false);
                sixthCheckText.gameObject.SetActive(false);
            }

            // Verifica se il quarto check (HDD montato) è completato
            if (assemblyManager.IsComponentInstalled("HDDSpostabile"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(false);
                secondCheckText.gameObject.SetActive(false);
                thirdCheckText.gameObject.SetActive(false);
                fourthCheckText.gameObject.SetActive(true);
                fifthCheckText.gameObject.SetActive(false);
                sixthCheckText.gameObject.SetActive(false);
            }

            // Verifica se il quinto check (Alimentatore montato) è completato
            if (assemblyManager.IsComponentInstalled("PSUSpostabile"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(false);
                secondCheckText.gameObject.SetActive(false);
                thirdCheckText.gameObject.SetActive(false);
                fourthCheckText.gameObject.SetActive(false);
                fifthCheckText.gameObject.SetActive(true);
                sixthCheckText.gameObject.SetActive(false);
            }

            // Verifica se il sesto check (Pannello montato) è completato
            if (assemblyManager.IsComponentInstalled("DoorSpostabile"))
            {
                zeroCheckText.gameObject.SetActive(false);
                firstCheckText.gameObject.SetActive(false);
                secondCheckText.gameObject.SetActive(false);
                thirdCheckText.gameObject.SetActive(false);
                fourthCheckText.gameObject.SetActive(false);
                fifthCheckText.gameObject.SetActive(false);
                sixthCheckText.gameObject.SetActive(true);
            }
        }
    }
}
