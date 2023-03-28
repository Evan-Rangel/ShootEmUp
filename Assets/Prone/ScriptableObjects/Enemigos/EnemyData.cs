using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private string description;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int attackType;
    [SerializeField] private int enemyDamage;
    [SerializeField] private int bulletType;
    [SerializeField] private int moveAttackType;
    [SerializeField] private float bulletTimer;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float projectileSpeed;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] BulletData bulletData;

    public string EnemyName { get { return enemyName; } }
    public string Description { get { return description; } }
    public int EnemyHealth { get { return enemyHealth; } }
    public int AttackType { get { return attackType; } }
    public int EnemyDamage { get { return enemyDamage; } }
    public int BulletType { get { return bulletType; } }
    public int MoveAttackType { get { return moveAttackType; } }
    public float BulletTimer { get { return bulletTimer; } }
    public float EnemySpeed { get { return enemySpeed; } }
    public int NumberOfProjectiles { get { return numberOfProjectiles; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public GameObject EnemyPrefab {  get { return enemyPrefab; } }
    public BulletData BulletData { get { return bulletData; } }
}
