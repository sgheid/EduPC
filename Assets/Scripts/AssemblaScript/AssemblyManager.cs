using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public GameObject assembledMotherboard; // La scheda madre finale
    public GameObject currentMotherboard; // La scheda madre da assemblare
    public DeskInteraction deskInteraction; // Riferimento alla gestione della modalità

    private bool isCPUInstalled = false;  // Flag per la CPU
    private bool isRAM1Installed = false; // Flag per la prima RAM
    private bool isRAM2Installed = false; // Flag per la seconda RAM
    private bool isCoolerInstalled = false; // Flag per il dissipatore

    private void Start()
    {
        // Assicuriamoci che la scheda madre assemblata sia inizialmente disattivata
        if (assembledMotherboard != null)
        {
            assembledMotherboard.SetActive(false);
        }
    }

    public void InstallComponent(string componentName)
    {
        switch (componentName)
        {
            case "CPU":
                isCPUInstalled = true;
                Debug.Log("CPU installata.");
                break;

            case "RAM1":
                if (isCPUInstalled)
                {
                    isRAM1Installed = true;
                    Debug.Log("Prima RAM installata.");
                }
                else
                {
                    Debug.LogWarning("Non puoi installare la RAM senza la CPU.");
                }
                break;

            case "RAM2":
                if (isCPUInstalled)
                {
                    isRAM2Installed = true;
                    Debug.Log("Seconda RAM installata.");
                }
                else
                {
                    Debug.LogWarning("Non puoi installare la RAM senza la CPU.");
                }
                break;

            case "CpuRaff":
                if (isCPUInstalled)
                {
                    isCoolerInstalled = true;
                    Debug.Log("Dissipatore installato.");
                }
                else
                {
                    Debug.LogWarning("Non puoi installare il dissipatore senza la CPU.");
                }
                break;

            default:
                Debug.LogError("Componente non riconosciuto: " + componentName);
                break;
        }

        CheckCompletion();
    }

    public bool CanInstallComponent(string componentName)
    {
        switch (componentName)
        {
            case "CPU":
                return true; // La CPU può essere installata in qualsiasi momento

            case "RAM1":
            case "RAM2":
                return isCPUInstalled; // La RAM richiede la CPU installata

            case "CpuRaff":
                return isCPUInstalled; // Il dissipatore richiede la CPU installata

            default:
                Debug.LogError("Componente non riconosciuto: " + componentName);
                return false;
        }
    }

    public bool IsComponentInstalled(string componentName)
    {
        switch (componentName)
        {
            case "CPU":
                return isCPUInstalled; // Restituisce true se la CPU è stata installata
            case "RAM1":
                return isRAM1Installed; // Restituisce true se RAM1 è stata installata
            case "RAM2":
                return isRAM2Installed; // Restituisce true se RAM2 è stata installata
            case "CpuRaff":
                return isCoolerInstalled; // Restituisce true se il CPU cooler è stato installato
            default:
                Debug.LogWarning($"Componente sconosciuto: {componentName}");
                return false; // Se il componente non è riconosciuto, restituisce false
        }
    }

    private void CheckCompletion()
    {
        if (isCPUInstalled && isRAM1Installed && isRAM2Installed && isCoolerInstalled)
        {
            FinishAssembly();
        }
    }

    public void FinishAssembly()
    {
        Debug.Log("Tutte le componenti sono state assemblate correttamente!");

        // Disattiva la scheda madre attuale
        if (currentMotherboard != null)
        {
            currentMotherboard.SetActive(false);
        }

        // Attiva la scheda madre assemblata
        if (assembledMotherboard != null)
        {
            assembledMotherboard.SetActive(true);
        }

        // Esci dalla modalità assemblaggio
        if (deskInteraction != null)
        {
            deskInteraction.ExitAssemblyMode();
            deskInteraction.isAssemblyComplete = true; // Imposta il flag per indicare che l'assemblaggio è completo
        }

        // Nascondi definitivamente il testo 3D del desk interaction
        if (deskInteraction != null && deskInteraction.interactionText3D != null)
        {
            deskInteraction.interactionText3D.SetActive(false);
        }
    }


}
