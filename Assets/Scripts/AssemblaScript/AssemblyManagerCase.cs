using UnityEngine;

public class AssemblyManagerCase : MonoBehaviour
{


    [Header("Oggetti richiesti")]
    public GameObject motherboard; // Scheda madre
    public GameObject gpu;         // GPU
    public GameObject psu;         // Alimentatore
    public GameObject hdd;         // Hard Disk
    public GameObject panel;       // Pannello finale

  

    [Header("Gestione modalit�")]
    public FreeRoamManager freeRoamManager; // Manager della modalit� free roam

    private bool isMotherboardPlaced = false;
    private bool isGPUPlaced = false;
    private bool isPSUPlaced = false;
    private bool isHDDPlaced = false;
    private bool isPanelPlaced = false;

    private void Start()
    {
     
    }

    public bool CanInstallComponent(string componentName)
    {
        switch (componentName)
        {
            case "MBSpostabile":
                return true; // La motherboard pu� essere sempre montata

            case "GPUSpostabile":
                return isMotherboardPlaced; // La GPU richiede che la motherboard sia montata

            case "PSUSpostabile":
                return true; // L'alimentatore pu� essere montato indipendentemente

            case "HDDSpostabile":
                return true; // L'HDD pu� essere montato indipendentemente

            case "DoorSpostabile":
                // Il pannello pu� essere montato solo se tutte le altre componenti sono posizionate
                return isMotherboardPlaced && isGPUPlaced && isPSUPlaced && isHDDPlaced;

            default:
                Debug.LogWarning($"Componente sconosciuta: {componentName}");
                return false;
        }
    }

    public void InstallComponent(string componentName)
    {
        switch (componentName)
        {
            case "MBSpostabile":
                if (!isMotherboardPlaced)
                {
                    isMotherboardPlaced = true;
                  
                    Debug.Log("Motherboard posizionata correttamente.");
                }
                break;

            case "GPUSpostabile":
                if (isMotherboardPlaced && !isGPUPlaced)
                {
                    isGPUPlaced = true;
                    
                    Debug.Log("GPU posizionata correttamente.");
                }
                break;

            case "PSUSpostabile":
                if (!isPSUPlaced)
                {
                    isPSUPlaced = true;
                    
                    Debug.Log("Alimentatore posizionato correttamente.");
                }
                break;

            case "HDDSpostabile":
                if (!isHDDPlaced)
                {
                    isHDDPlaced = true;
                
                    Debug.Log("HDD posizionato correttamente.");
                }
                break;

            case "DoorSpostabile":
                if (isMotherboardPlaced && isGPUPlaced && isPSUPlaced && isHDDPlaced && !isPanelPlaced)
                {
                    isPanelPlaced = true;
                  
                    Debug.Log("Pannello finale posizionato correttamente.");
                }
                break;

            default:
                Debug.LogWarning($"Componente sconosciuta: {componentName}");
                break;
        }

      
    }

    public bool IsComponentInstalled(string componentName)
    {
        switch (componentName)
        {
            case "MBSpostabile":
                return isMotherboardPlaced; // Verifica se la scheda madre � stata montata
            
            case "GPUSpostabile":
                return isGPUPlaced; // Verifica se la GPU � stata montata
            case "HDDSpostabile":
                return isHDDPlaced; // Verifica se l'HDD � stato montato
            case "PSUSpostabile":
                return isPSUPlaced; // Verifica se l'alimentatore � stato montato
            case "DoorSpostabile":
                return isPanelPlaced; // Verifica se il pannello finale � stato montato
            default:
                return false; // Componente sconosciuto
        }
    }



}
