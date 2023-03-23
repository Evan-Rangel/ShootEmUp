using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private spaceship666 player;
    private float baseGravity;

    [Header("Dash")]
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float timeCanDash = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    public bool IsDashing => isDashing;

    private bool dashingBool;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<spaceship666>();
        baseGravity = rb.gravityScale;
    }

    void Update()
    {
        if (playerInput.actions["DashRight"].WasPressedThisFrame())
        {

            if (player.DirectionX < 0f)
            {
                StartCoroutine(DashRightWhenMovingLeft());
            }
            else if (player.DirectionX > 0f)
            {
                StartCoroutine(DashRightWhenMovingRight());
            }
            else
            {

                StartCoroutine(DashRight());
            }
            
        }

        if (playerInput.actions["DashLeft"].WasPressedThisFrame())
        {
            if (player.DirectionX > 0f)
            {
                StartCoroutine(DashLeftWhenMovingRight());
            }
            else if (player.DirectionX < 0f)
            {
                StartCoroutine(DashLeftWhenMovingLeft());
            }
            else
            {
                StartCoroutine(DashLeft());
            }
        }
    }

    private IEnumerator DashRight()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce + dashForce, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }
        
    }

    private IEnumerator DashRightWhenMovingLeft()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce + dashForce * 2f, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }

    }

    private IEnumerator DashRightWhenMovingRight()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce + dashForce / 2f, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }

    }


    private IEnumerator DashLeft()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce - dashForce, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }

    }
    private IEnumerator DashLeftWhenMovingRight()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce - dashForce * 2f, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }

    }

    private IEnumerator DashLeftWhenMovingLeft()
    {
        if (/*player.DirectionX != 0 && */canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.DirectionX * dashForce - dashForce / 2f, player.DirectionY * player.Speed);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }

    }
}
