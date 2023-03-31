using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBosses : MonoBehaviour
{
    [SerializeField] ControladorDeEnemigos referenciaCE;
    [SerializeField] GameObject[] enemyParts;
    [SerializeField] private AnimationClip aEspecialAnimation;
    [SerializeField] public AudioClip ataqueE;
    [SerializeField] public AudioClip bossMusic;
    bool salirDeFase1 = false;
    bool salirDeFase2 = false;
    public bool ganastePlayer = false;

    private void Start()
    {
       // Musica.InstanceSonidos.EjecutarMusica(bossMusic, 0.2f);
        enemyParts[0].GetComponent<PolygonCollider2D>().enabled = false;
        enemyParts[1].GetComponent<PolygonCollider2D>().enabled = false;
        enemyParts[2].GetComponent<PolygonCollider2D>().enabled = false;
        enemyParts[3].GetComponent<PolygonCollider2D>().enabled = false;
        referenciaCE.enabled = false;
        StartCoroutine(EsperarIntroduccion());
    }
    void Update()
    {
        //Fase 1
        Fase1();

        //Fase 2
        Fase2();

        //Fase3
        Fase3();

    }

    private void Fase1()
    {
        if (enemyParts[1].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0)
        {
            enemyParts[2].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.2f;
            enemyParts[0].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.1f;
        }
        if (enemyParts[2].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0)
        {
            enemyParts[1].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.2f;
            enemyParts[0].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.1f;
        }
    }

    private void Fase2()
    {
        if (enemyParts[1].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0 && enemyParts[2].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0 && enemyParts[0].GetComponent<ControladorDeEnemigos>().enemyHealth > 0)
        {
            enemyParts[0].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase2", true);
            enemyParts[3].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase2", true);
            if (salirDeFase1 == false)
            {
                enemyParts[0].GetComponent<ControladorDeEnemigos>().enabled = false;
                enemyParts[3].GetComponent<ControladorDeEnemigos>().enabled = false;
                StartCoroutine(EsperarFase2());
            }
            if (enemyParts[0].GetComponent<ControladorDeEnemigos>().enemyHealth <= 5)
            {
                enemyParts[0].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase2Mitad", true);
                enemyParts[3].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase2Mitad", true);
                enemyParts[0].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.4f;
                enemyParts[3].GetComponent<ControladorDeEnemigos>().bulletTimer = 2.5f;
            }
        }
    }

    private void Fase3()
    {
        if (enemyParts[0].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0)
        {
            enemyParts[3].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase3", true);
            if (salirDeFase2 == false)
            {
                enemyParts[3].GetComponent<ControladorDeEnemigos>().enabled = false;
                StartCoroutine(EsperarFase3());
            }
            if (enemyParts[3].GetComponent<ControladorDeEnemigos>().enemyHealth <= 5)
            {
                enemyParts[4].SetActive(true);
                enemyParts[3].GetComponent<ControladorDeEnemigos>().animator.SetBool("Fase3Mitad", true);
                StartCoroutine(AtqueEspecial());
                enemyParts[3].GetComponent<ControladorDeEnemigos>().attackType = 1;
                enemyParts[3].GetComponent<ControladorDeEnemigos>().bulletInitialAngle = 0;
                enemyParts[3].GetComponent<ControladorDeEnemigos>().bulletTimer = 1.4f;
                enemyParts[3].GetComponent<ControladorDeEnemigos>().numberOfProyectiles = 10;
            }
            if (enemyParts[3].GetComponent<ControladorDeEnemigos>().enemyHealth <= 0)
            {
                //Musica.InstanceSonidos.PararMusica();
                //ControladorDeSonidos.InstanceSonidos.PararSonidos();
                enemyParts[4].SetActive(false);
                ganastePlayer = true;
                UIManager.instance.WinScreen();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player117>().Ganaste();
            }
        }
    }

    public IEnumerator EsperarIntroduccion()
    {
        yield return new WaitForSeconds(24);
        enemyParts[1].GetComponent<PolygonCollider2D>().enabled = true;
        enemyParts[2].GetComponent<PolygonCollider2D>().enabled = true;
        enemyParts[0].GetComponent<ControladorDeEnemigos>().enabled = true;
        enemyParts[1].GetComponent<ControladorDeEnemigos>().enabled = true;
        enemyParts[2].GetComponent<ControladorDeEnemigos>().enabled = true;
    }

    IEnumerator EsperarFase2()
    {
        yield return new WaitForSeconds(5);
        referenciaCE.enabled = true;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().numberOfProyectiles = 1;
        enemyParts[0].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.5f;
        enemyParts[0].GetComponent<ControladorDeEnemigos>().numberOfProyectiles = 5;
        enemyParts[0].GetComponent<PolygonCollider2D>().enabled = true;
        salirDeFase1 = true;
    }

    IEnumerator EsperarFase3()
    {
        yield return new WaitForSeconds(5);
        referenciaCE.enabled = true;
        enemyParts[3].GetComponent<PolygonCollider2D>().enabled = true;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().numberOfProyectiles = 3;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().angleSum = 10;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().bulletInitialAngle = 145;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().bulletTimer = 0.4f;
        enemyParts[3].GetComponent<ControladorDeEnemigos>().attackType = 4;        
        salirDeFase2 = true;
    }

    IEnumerator AtqueEspecial()
    {
        yield return new WaitForSeconds(aEspecialAnimation.length);
        enemyParts[4].GetComponent<BoxCollider2D>().enabled = true;
       enemyParts[3].GetComponent<ControladorDeEnemigos>().animator.SetBool("Especial", true);
       // ControladorDeSonidos.InstanceSonidos.EjecutarSonidos(ataqueE, 0.12f);
    }
}
