using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    public int GetHealth { get { return enemyHealth; } }
    public void RecibirDanio(int danio)
    {
        enemyHealth = enemyHealth - danio;
    }
}
