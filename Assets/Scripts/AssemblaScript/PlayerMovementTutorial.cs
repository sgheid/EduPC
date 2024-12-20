using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool canMove = true; // Flag per abilitare/disabilitare il movimento

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (canMove)
        {
            MyInput();
        }
        else
        {
            horizontalInput = 0; // Resetta l'input
            verticalInput = 0;
        }

        SpeedControl();
        rb.drag = groundDrag; // Applica il drag costante
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer(); // Movimento
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Calcola la direzione del movimento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Aggiungi forza per muovere il player
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limita la velocità se necessario
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    // Metodo per abilitare/disabilitare il movimento
    public void SetMovementState(bool state)
    {
        canMove = state;

        if (!state)
        {
            rb.velocity = Vector3.zero; // Ferma immediatamente il player
        }
    }
}
