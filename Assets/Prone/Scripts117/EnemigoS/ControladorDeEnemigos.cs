using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeEnemigos : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    Vector2 startPoint;
    const float radius = 1;
    float timer;
    int angleSum = 0;
    int angleSumI = 0;
    int multiplicador;

    [SerializeField] int enemyHealth;

    private PolygonCollider2D colliderEnemigo;
    [SerializeField] private AnimationClip animacionMorir;
    private Animator animator;

    private void Start()
    {

        enemyHealth = enemyData.EnemyHealth;
        //Tipo de Disparo
        timer = enemyData.BulletTimer;
        colliderEnemigo = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();

        switch (enemyData.AttackType)
        {
            case 1:
                multiplicador = 0;
                break;
            case 2:
                multiplicador = 20;
                break;
            case 3:
                multiplicador = 60;
                break;
            default:
                break;
        }
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
            SpawnProjectiles(angleSum);
            angleSum -= multiplicador;

            SpawnProjectiles(angleSumI);
            angleSumI += multiplicador;
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
    private void SpawnProjectiles(int addAngle)
    {
        float angleStep = 360f / enemyData.NumberOfProjectiles;
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
