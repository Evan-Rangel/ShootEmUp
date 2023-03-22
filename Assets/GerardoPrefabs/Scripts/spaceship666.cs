using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceship666 : MonoBehaviour
{
    [SerializeField] private float BulletSpawn = 0.8f;


    Rigidbody2D rb;
    private Vector2 movementInput;
    public float speed;
    private PlayerDash playerDash;

    private float directionX;
    private float directionY;
    public float DirectionX => directionX;
    public float DirectionY => directionY;
    public float Speed => speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDash = GetComponent<PlayerDash>();
    }
  
    void Start()
    {

    }


    void Update()
    {
        //rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        //rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));

        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        //movementInput = new Vector2(movementX, movementY).normalized;


        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laser = ShotPool.Instance.RequestLaser();
            laser.transform.position = transform.position + Vector3.up * BulletSpawn;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(directionX * speed, directionY * speed);
    }

    private void FixedUpdate()
    {
        if (!playerDash.IsDashing)
        {
            Move();
        }

    }

    public class inputManager : MonoBehaviour
    {
        InputControl control;

        private void Awake()
        {
            control = new InputControl();
        }

        private void OnEnable()
        {
            control.Gameplay.Enable();
            control.Gameplay.Movement.performed += movement;
            control.Gameplay.Movement.canceled += movement;
        }

        private void movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Vector2 input = obj.ReadValue<Vector2>();

        }
    }

}
