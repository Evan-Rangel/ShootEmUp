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
        //Debug.Log("Entra3");
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
        enemysAlive--;
        if (enemysAlive<=0)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        for (int i = 0; i < waveObjects.Count; i++)
        {
            if (waveObjects[i].activeSelf == false)
            {
                waveObjects.RemoveAt(i);
                i = 0;
            }
        }
        currentWave++;
        enemysAlive = enemysWave[currentWave];
        StartCoroutine(Posicionar());
    }
    IEnumerator Posicionar()
    {
        Debug.Log(enemysWave[currentWave]);
        for (int i = 0; i < enemysWave[currentWave]; i++)
        {
            waveObjects[i].GetComponent<ControladorMovimiento>().enabled = true;
        }
        yield return new WaitForSeconds(3);
        ActivarColliders();
        StopCoroutine(Posicionar());
    }
}
