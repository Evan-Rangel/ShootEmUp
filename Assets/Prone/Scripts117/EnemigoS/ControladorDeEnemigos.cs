using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeEnemigos : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Vector2 startPoint;
    const float radius = 1;
    float timer;
    int angleSum;
    int cont = 0;
    int mult = 1;
    [SerializeField] int enemyHealth;

    private PolygonCollider2D colliderEnemigo;
    [SerializeField] private AnimationClip animacionMorir;
    private Animator animator;

    private void Start()
    {
        angleSum = enemyData.BulletInitialAngle;
        enemyHealth = enemyData.EnemyHealth;
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

            switch (enemyData.AttackType)
            { 
                case 1: case 2://Ataque 1 y 2, solo dependen del bulletAngleSum
                    SpawnProjectiles(angleSum, 360 / enemyData.NumberOfProjectiles);
                    angleSum += enemyData.BulletAngleSum;
                    timer = enemyData.BulletTimer;
                break;
            case 3://Ataque 3 es petalo
                
                    SpawnProjectiles(angleSum, 360 / enemyData.NumberOfProjectiles);
                    SpawnProjectiles(-angleSum, 360 / enemyData.NumberOfProjectiles);
                    angleSum += enemyData.BulletAngleSum;

                    //angleSumI -= enemyData.BulletAngleSum;
                    timer = enemyData.BulletTimer;
                break;
            case 4: //Ataque 4 es vuelta y regreso
                    if (cont == 6)
                    {
                        mult = -mult;
                        cont = 0;
                    }
                    SpawnProjectiles(angleSum, 360/enemyData.DistanceBetweenProjectiles);
                    angleSum += mult*enemyData.BulletAngleSum;
                    cont++;
                    timer = enemyData.BulletTimer;
                break;
            default:
                break;
            }
            timer = enemyData.BulletTimer;
        }


    }
    //Funcion de Recibir daño (Movi esto Evan)
    public void RecibirDanio(int danio)
    {
        enemyHealth = enemyHealth - danio;
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
        
        for (int i = 0; i < enemyData.NumberOfProjectiles; i++)
        {
            
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector2 projectileVector = new Vector2(projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * enemyData.ProjectileSpeed;

            GameObject tmpObj = DisparoPool117.Instance.RequestLaser();
            tmpObj.GetComponent<Disparo117>().SetProps(projectileMoveDirection, transform.position, -angle, enemyData.BulletData);

            angle += angleStep;

        }
    }

}