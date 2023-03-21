using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    [SerializeField] private string bulletName;
    [SerializeField] private string description;
    [SerializeField] private string tagName;
    [SerializeField] private int bulletType;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] UnityEditor.Animations.AnimatorController animator;
    [SerializeField] private Sprite sprite;

    public string BulletName { get { return bulletName; } }
    public string Description { get { return description; } }
    public string TagName { get { return tagName; } }
    public int BulletType { get { return bulletType; } }
    public int BulletDamage { get { return bulletDamage; } }
    public float BulletSpeed { get { return bulletSpeed; } }
    public UnityEditor.Animations.AnimatorController Animator { get { return animator; } }
    public Sprite Sprite { get { return sprite; } }
}
