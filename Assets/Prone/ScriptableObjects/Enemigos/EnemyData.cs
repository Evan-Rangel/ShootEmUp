using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Atributes")]
    [SerializeField] private string enemyName;
    [SerializeField] private string description;
    [SerializeField] private int enemyHealth;
    [Tooltip("Este daño es fisico, se le hace al jugador si el enemigo toca al player, el daño de la bala esta en el ScriptableObject de la bala")]
    [SerializeField] private int enemyDamage;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int attackType;
    [SerializeField] private int moveAttackType;
    [SerializeField] private int score;
    [Header("Enemy Bullet Atribute")]
    [Tooltip("Este daño es el de la bala del PLAYER el enemigo tiene su propia variable de daño")]
    [SerializeField] private int bulletDamage;
    [Tooltip("El tipo de bala que disparara todos los tipos de disparo son de enemigos menos el 0, ese es para el player")]
    [SerializeField] private int bulletType;
    [Tooltip("Suma de angulo para dar efecto de giro, si es 0 sale recto")]
    [SerializeField] private int bulletAngleSum;
    [Tooltip("Angulo en que inicia a disparar el enemigo, luego hado documentacion de angulos")]
    [SerializeField] private int bulletInitialAngle;
    [Tooltip("El timer es la cadencia de disparo en segundos se recomienda el (0.25)")]
    [SerializeField] private float bulletTimer;
    [Tooltip("Numero de balas que disparar el jugador....no mames esa es obvio")]
    [SerializeField] private int numberOfProjectiles;
    [Tooltip("Este solo es para el disparo tipo 4, mientras mas alto el numero, mas cerca estan")]
    [SerializeField, Range(0, 100)] private int distanceBetweenProjectiles;
    [Tooltip("Velocidad de disparo entre cada disparo")]
    [SerializeField] private float projectileSpeed;
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject enemyPrefab;
    [Header("Bullet Datas")]
    [SerializeField] BulletData bulletData;

    public int Score { get { return score; } }
    public string EnemyName { get { return enemyName; } }
    public string Description { get { return description; } }
    public int EnemyHealth { get { return enemyHealth; } }
    public int EnemyDamage { get { return enemyDamage; } }
    public float EnemySpeed { get { return enemySpeed; } }
    public int AttackType { get { return attackType; } }
    public int BulletAngleSum { get { return bulletAngleSum; } }
    public int BulletInitialAngle { get { return bulletInitialAngle; } }
    public int DistanceBetweenProjectiles { get { return distanceBetweenProjectiles; } }
    public int MoveAttackType { get { return moveAttackType; } }
    public int BulletDamage { get { return bulletDamage; } }
    public int BulletType { get { return bulletType; } }
    public float BulletTimer { get { return bulletTimer; } }
    public int NumberOfProjectiles { get { return numberOfProjectiles; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public GameObject EnemyPrefab { get { return enemyPrefab; } }
    public BulletData BulletData { get { return bulletData; } }
}
