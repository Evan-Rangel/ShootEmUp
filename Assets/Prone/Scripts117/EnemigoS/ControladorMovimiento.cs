using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMovimiento : MonoBehaviour
{
    [SerializeField] MovementData movementData;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dTime;
    int typeMovement;
    float velocity;
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
                velocity = 1;
                direccion = Vector2.down * velocity * movementData.Velocity;

                break;
            case 2:
                velocity = 1;

                direccion = Vector2.up * velocity * movementData.Velocity;
                break;
            case 3:
                velocity = 1;
                direccion = Vector2.right * velocity * movementData.Velocity;
                break;
            case 4:
                velocity = 1;

                direccion = Vector2.left * velocity * movementData.Velocity;
                break;
            case 5: //Esquina Derecha Arriba
                velocity = 1;

                direccion = new Vector2(1,1) * velocity * movementData.Velocity;
                break;
            case 6://Esquina Derecha Abajo
                velocity = 1;

                direccion = new Vector2 (1,-1) * velocity * movementData.Velocity;
                break;
            case 7: //Esquina Izquierda Arriba
                velocity = 1;

                direccion = new Vector2(-1, 1) * velocity * movementData.Velocity;
                break;
            case 8://Esquina Izquierda Abajo
                velocity = 1;

                direccion = new Vector2(-1, -1) * velocity * movementData.Velocity;
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
