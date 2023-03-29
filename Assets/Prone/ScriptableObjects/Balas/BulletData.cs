using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("Bullet Atributes for the Pool")]
    [SerializeField] private string bulletName;
    [SerializeField] private string description;
    [Tooltip("Este daño es el de la bala de quien la dispara")]
    [SerializeField] private int bulletDamage;
    [Header("Bullet Atributes for the change of bullet Between Player and Enemies")]
    [SerializeField] private string tagName;
    [SerializeField] UnityEditor.Animations.AnimatorController animator;
    [SerializeField] private Sprite sprite;

    public string BulletName { get { return bulletName; } }
    public string Description { get { return description; } }
    public int BulletDamage { get { return bulletDamage; } }
    public string TagName { get { return tagName; } }
    public UnityEditor.Animations.AnimatorController Animator { get { return animator; } }
    public Sprite Sprite { get { return sprite; } }
}
