using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeEnemigos : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Vector2 startPoint;
    const float radius = 1;
    float timer;
    public int angleSum;
    int cont = 0;
    int mult = 1;
    public  int enemyHealth;
    [SerializeField] public int attackType;
    [SerializeField] public int numberOfProyectiles;
    [SerializeField] public float bulletTimer;
    [SerializeField] public int bulletInitialAngle;

    private PolygonCollider2D colliderEnemigo;
    [SerializeField] private AnimationClip animacionMorir;
    public  Animator animator;

    //Animacion de Parpadeo
    public float tiempo_brillo;
    public SpriteRenderer[] spr;
    public bool cambio;
    public Color[] color_;
    public float speed_shine;
    public float cronometro;

    //Sonidos
    [SerializeField] private AudioClip disparoSonido;
    [SerializeField] public AudioClip dañoSonido;
    [SerializeField] public AudioClip morirSonido;
    bool activarSM = false;

    private void Start()
    {
        angleSum = enemyData.BulletInitialAngle;
        enemyHealth = enemyData.EnemyHealth;
        attackType = enemyData.AttackType;
        numberOfProyectiles = enemyData.NumberOfProjectiles;
        bulletTimer = enemyData.BulletTimer;
        bulletInitialAngle = enemyData.BulletInitialAngle;

        //Tipo de Disparo
        timer = enemyData.BulletTimer;
        colliderEnemigo = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();

        //Tipo de Movimiento
    }
    //Cuando Muere el enemigo se activa la animacion de morir, se descativa el collider para que no estorbe las balas y se activa la corrutina de la desactivacion del enemigo
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            animator.SetBool("Muerto", true);
            colliderEnemigo.enabled = false;
            StartCoroutine(desactivarEnemigo());
        }
    }

    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {

            switch (attackType)
            {
                case 1:
                case 2://Ataque 1 y 2, solo dependen del bulletAngleSum
                    SpawnProjectiles(bulletInitialAngle, 360 / numberOfProyectiles);
                    bulletInitialAngle += angleSum;
                    timer = bulletTimer;
                    break;
                case 3://Ataque 3 es petalo

                    SpawnProjectiles(bulletInitialAngle, 360 / numberOfProyectiles);
                    SpawnProjectiles(-bulletInitialAngle, 360 / numberOfProyectiles);
                    bulletInitialAngle += angleSum;

                    //angleSumI -= enemyData.BulletAngleSum;
                    timer = bulletTimer;
                    break;
                case 4: //Ataque 4 es vuelta y regreso
                    if (cont == 6)
                    {
                        mult = -mult;
                        cont = 0;
                    }
                    SpawnProjectiles(bulletInitialAngle, 360 / enemyData.DistanceBetweenProjectiles);
                    bulletInitialAngle += mult * angleSum;
                    cont++;
                    timer = bulletTimer;
                    break;
                default:
                    break;
            }
            timer = bulletTimer;
        }


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

    //Funcion de Recibir daño (Movi esto Evan)
    public void RecibirDanio(int danio)
    {
        enemyHealth = enemyHealth - danio;
        //ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.12f);
    }
    //Desactivar el Objeto Luego de la animacion
    IEnumerator desactivarEnemigo()
    {
        yield return new WaitForSeconds(animacionMorir.length);
        gameObject.SetActive(false);
    }
    //Movimiento, rotacion, posicion y Spawneo de balas
    private void SpawnProjectiles(int addAngle, float _angleStep)
    {
        float angleStep = _angleStep;
        float angle = addAngle;
        startPoint = transform.position;

        for (int i = 0; i < numberOfProyectiles; i++)
        {

            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * enemyData.ProjectileSpeed;

            GameObject tmpObj = DisparoPool117.Instance.RequestLaser();
           // ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(disparoSonido, 0.08f);
            tmpObj.GetComponent<Disparo117>().SetProps(projectileMoveDirection, transform.position, -angle, enemyData.BulletData);

            angle += angleStep;

        }
    }

}