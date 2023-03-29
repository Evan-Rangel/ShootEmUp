using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player117 : MonoBehaviour
{
    [Header("Bullet Data")]
    [SerializeField] BulletData bulletData;
    //Limites Variables
    [Header("Player Limits")]
    public Boundary boundary;
    //Personaje Variables
    [Header("Player Atributes")]
    [SerializeField] int life;
    [SerializeField] private float speed;
    float moveX;
    float moveY;
    private Vector2 moveInput;
    private PolygonCollider2D colliderPlayer;
    private Rigidbody2D rb;
    //Disparo Variables
    [Header("Player Bullet Atributes")]
    [SerializeField] private float laserOffset;
    [SerializeField] float bulletSpeed;
    [SerializeField] int shotLevel;
    [SerializeField] private Transform[] shotSpawns;
    //Animaciones
    [Header("Player Animation Atributes")]
    [SerializeField] private AnimationClip animacionMorir;
    private Animator animatorPlayer;

    void Start()
    {
        //Rb del Personaje
        rb = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<PolygonCollider2D>();
        //Animator del Personaje
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        //Vida del Player
        if (life <= 0)
        {
            animatorPlayer.SetBool("Morir", true);
            colliderPlayer.enabled = false;
            StartCoroutine(desactivarPlayer());
        }

        //Movimiento del Personaje
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        animatorPlayer.SetFloat("MovX", moveX); //Animacion del Personaje
        moveInput = new Vector2(moveX, moveY).normalized;

        //Disparo
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laser;
            if (shotLevel == 1 || shotLevel == 3)
            {
                laser = DisparoPool117.Instance.RequestLaser();
                laser.GetComponent<Disparo117>().SetPropsPlayer(new Vector2(shotSpawns[0].position.x, shotSpawns[0].position.y) + Vector2.up * laserOffset, bulletData);
                laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
                //laser.transform.position = shotSpawns[0].transform.position + Vector3.up * laserOffset;
                //laser.transform.rotation = gameObject.transform.rotation;

            }
            if (shotLevel == 2 || shotLevel == 3)
            {
                laser = DisparoPool117.Instance.RequestLaser();
                laser.GetComponent<Disparo117>().SetPropsPlayer(new Vector2(shotSpawns[1].position.x, shotSpawns[1].position.y) + Vector2.up * laserOffset, bulletData);
                laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
                laser = DisparoPool117.Instance.RequestLaser();
                laser.GetComponent<Disparo117>().SetPropsPlayer(new Vector2(shotSpawns[2].position.x, shotSpawns[2].position.y) + Vector2.up * laserOffset, bulletData);
                laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
            }

        }
    }

    private void FixedUpdate()
    {
        //Movimiento del Personaje
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax));
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    public void RecibirDanio(int danio)
    {
        life = life - danio;
    }

    IEnumerator desactivarPlayer()
    {
        yield return new WaitForSeconds(animacionMorir.length);
        gameObject.SetActive(false);
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && transform.CompareTag("BulletEnemy"))
        {
            life = life - 1;
        }

        if (other.CompareTag("Paredes"))
        {
            other.GetComponent<Disparo117>().ResetProps();           
        }
    }
    */
    //Desactivar el Objeto Luego de la animacion

}
