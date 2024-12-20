using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    private bool canControlCamera = true; // Flag per abilitare/disabilitare il controllo della camera

    public void Start()
    {
        // Blocca il cursore e lo nasconde di default
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Se il controllo della camera è disabilitato, esci
        if (!canControlCamera) return;

        // Controllo del movimento del mouse
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applica la rotazione alla camera e all'orientamento
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // Metodo per abilitare/disabilitare il controllo della camera
    public void SetCameraControlState(bool state)
    {
        canControlCamera = state;
    }

    // Metodo per sbloccare e mostrare il cursore
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Sblocca il cursore
        Cursor.visible = true; // Rende visibile il cursore
    }

    // Metodo per bloccare e nascondere il cursore
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Blocca il cursore
        Cursor.visible = false; // Nasconde il cursore
    }
}
