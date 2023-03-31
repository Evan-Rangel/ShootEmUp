using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMovimiento : MonoBehaviour
{
    [SerializeField] MovementData movementData;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dTime;
    int typeMovement;
    Vector2 direccion;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        typeMovement = 1;
        MovementPatterns();
        StartCoroutine(ChangeDirection());

    }
    private void Update()
    {

        Movement();
    }

    private void MovementPatterns()
    {
        switch (typeMovement)
        {
            case 1:
                direccion = Vector2.down * movementData.Velocity;

                break;
            case 2:

                direccion = Vector2.up *  movementData.Velocity;
                break;
            case 3:
                direccion = Vector2.right *  movementData.Velocity;
                break;
            case 4:

                direccion = Vector2.left * movementData.Velocity;
                break;
            case 5: //Esquina Derecha Arriba

                direccion = new Vector2(1,1) *  movementData.Velocity;
                break;
            case 6://Esquina Derecha Abajo

                direccion = new Vector2 (1,-1) * movementData.Velocity;
                break;
            case 7: //Esquina Izquierda Arriba

                direccion = new Vector2(-1, 1) * movementData.Velocity;
                break;
            case 8://Esquina Izquierda Abajo

                direccion = new Vector2(-1, -1) * movementData.Velocity;
                break;
            case 9:
                direccion = Vector2.zero;
                break;
            default:
                break;
        }
    }
    
    private void Movement()
    {
        rb.velocity = direccion;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paredes"))
        {
            direccion = Vector2.zero;
        }
    }
    IEnumerator ChangeDirection()
    {
        for (int i = 0; i < movementData.TypeMovemet.Length; i++)
        {
            yield return new WaitForSeconds(movementData.TimeMovemet[i]);
            typeMovement = movementData.TypeMovemet[i];
            MovementPatterns();
        }
        StopCoroutine(ChangeDirection());
    }
}
