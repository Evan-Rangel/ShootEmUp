using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlOleadas : MonoBehaviour
{
    [SerializeField] List <GameObject> waveObjects;
    [SerializeField] int currentWave = 0;
    [SerializeField] int[] enemysWave;
    [SerializeField] int enemysAlive;

    private static ControlOleadas instanceOleadas;
    public static ControlOleadas InstanceOleadas { get { return instanceOleadas; } }
    private void Awake()
    {
        if (instanceOleadas == null)
        {
            instanceOleadas = this;
        }
        else
        {
            Destroy(gameObject);
        }
        enemysAlive = enemysWave[currentWave];
    }
    private void Start()
    {
        StartCoroutine(Posicionar());
    }
    void ActivarColliders()
    {
        for (int i = 0; i < enemysWave[currentWave]; i++)
        {
            waveObjects[i].GetComponent<PolygonCollider2D>().enabled = true;
            waveObjects[i].GetComponent<ControladorDeEnemigos>().enabled = true;
            
        }
    }
    public void EnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false);
        waveObjects.Remove(enemy);
        enemysAlive--;
        if (enemysAlive<=0)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave>= enemysWave.Length)
        {
            
            UIManager.instance.WinScreen();
        }
        else
        {
            enemysAlive = enemysWave[currentWave];
            StartCoroutine(Posicionar());
        }
    }
    IEnumerator Posicionar()
    {
        for (int i = 0; i < enemysWave[currentWave]; i++)
        {
           // if (waveObjects[i].GetComponent<ControladorBosses>())
            {
             //   waveObjects[i].GetComponent<ControladorBosses>().enabled = true;
            }
           // else
            {
                waveObjects[i].GetComponent<ControladorMovimiento>().enabled = true;
            }
        }
        yield return new WaitForSeconds(3);
        //if (!waveObjects[0].GetComponent<ControladorBosses>())
        {
            ActivarColliders();
        }
        StopCoroutine(Posicionar());
    }
}
