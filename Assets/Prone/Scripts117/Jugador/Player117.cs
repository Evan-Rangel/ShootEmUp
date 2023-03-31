using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player117 : MonoBehaviour
{
    [SerializeField] ControladorBosses referenciaCB;
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
    private Vector2 moveInput;
    private PolygonCollider2D colliderPlayer;
    private Rigidbody2D rb;
    [SerializeField] PlayerInput playerInput;
    //Disparo Variables
    [Header("Player Bullet Atributes")]
    [SerializeField] private float laserOffset;
    [SerializeField] float bulletSpeed;
    public int shotLevel;
    [SerializeField] private Transform[] shotSpawns;
    //Animaciones
    [Header("Player Animation Atributes")]
    [SerializeField] private AnimationClip animacionMorir;
    private Animator animatorPlayer;

    //Sonidos
    [SerializeField] private AudioClip disparoSonido;
    [SerializeField] public AudioClip dañoSonido;
    [SerializeField] public AudioClip morirSonido;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip bossGanaste;
    bool activarSM = false;

    //Animacion de Parpadeo
    public float tiempo_brillo;
    public SpriteRenderer[] spr;
    public bool cambio;
    public Color[] color_;
    public float speed_shine;
    public float cronometro;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        //Rb del Personaje
        rb = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<PolygonCollider2D>();
        //Animator del Personaje
        animatorPlayer = GetComponent<Animator>();
        UIManager.Instance.SetHeats(life);
    }

    void Update()
    {
        Brillo();
        //Vida del Player
        if (life <= 0 && !activarSM)
        {
            speed = 0;
            animatorPlayer.SetBool("Morir", true);
            colliderPlayer.enabled = false;
            //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(morirSonido, 0.2f);
            UIManager.instance.LoseScreen();
            activarSM = true;
            StartCoroutine(desactivarPlayer());
        }

        if (playerInput.actions["GodMode"].WasPressedThisFrame() && !activarSM)
        {
            activarSM = true;
        }

        //Movimiento del Personaje
        moveX = playerInput.actions["Move"].ReadValue<Vector2>().x;
        animatorPlayer.SetFloat("MovX", moveX); //Animacion del Personaje
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>().normalized;

        //Disparo
        if (playerInput.actions["Fire"].WasPressedThisFrame())
        {
            //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.15f);
            GameObject laser;
            if (shotLevel == 1 || shotLevel == 3)
            {
                laser = DisparoPool117.Instance.RequestLaser();
                //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.15f);
                laser.GetComponent<Disparo117>().SetPropsPlayer(new Vector2(shotSpawns[0].position.x, shotSpawns[0].position.y) + Vector2.up * laserOffset, bulletData);
                laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;                
                //laser.transform.position = shotSpawns[0].transform.position + Vector3.up * laserOffset;
                //laser.transform.rotation = gameObject.transform.rotation;

            }
            if (shotLevel == 2 || shotLevel == 3)
            {
                laser = DisparoPool117.Instance.RequestLaser();
                //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.15f);
                laser.GetComponent<Disparo117>().SetPropsPlayer(new Vector2(shotSpawns[1].position.x, shotSpawns[1].position.y) + Vector2.up * laserOffset, bulletData);
                laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;
                laser = DisparoPool117.Instance.RequestLaser();
                //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.15f);
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

    //Brilo de daño
    public void Brillo()
    {
        if (cronometro > 0)
        {
            cronometro -= 1 * Time.deltaTime;
            spr[1].sprite = spr[0].sprite;
            tiempo_brillo += speed_shine * Time.deltaTime;

            switch (cambio)
            {
                case true:

                    spr[1].color = color_[0];
                    break;

                case false:
                    spr[1].color = color_[1];
                    break;
            }

            if (tiempo_brillo > 1)
            {
                cambio = !cambio;
                tiempo_brillo = 0;
            }
        }
        else
        {
            cronometro = 0;
            spr[1].color = color_[0];
        }
    }
 
    public void RecibirDanio(int danio)
    {
        if (!activarSM)
        {
            UIManager.Instance.TakeDamage();

            life = life - danio;
            //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(dañoSonido, 0.2f);
            Recuperarse();
            cronometro = 1.5f;
        }
    }

    public void Recuperarse()
    {
        colliderPlayer.enabled = false;
        StartCoroutine(reactivarColliderPlayer());
    }

    public void Ganaste()
    {
        animatorPlayer.SetBool("Ganar", true);
       // Musica.InstanceSonidos.EjecutarMusica(bossGanaste, 0.2f);
    }

    IEnumerator reactivarColliderPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        colliderPlayer.enabled = true;
    }

    IEnumerator desactivarPlayer()
    {
        yield return new WaitForSeconds(animacionMorir.length);
        gameObject.SetActive(false);
    }

}
