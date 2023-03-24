using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Disparo117 : MonoBehaviour
{
    public BulletData bulletData;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private GameObject[] typesBullets;

    private void OnEnable()
    {
        //bulletRB.velocity = Vector2.up * bulletData.BulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigos") && gameObject.CompareTag("BulletPlayer"))
        {
            other.GetComponent<EnemyHealth>().RecibirDanio(bulletData.BulletDamage);
            gameObject.tag = "Untagged";
            gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
            bulletRB.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Paredes"))
        {
            gameObject.tag = "Untagged";
            gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
            bulletRB.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    public void SetProps()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData.Sprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData.Animator;
        gameObject.tag = bulletData.TagName;
        Debug.Log(gameObject.tag);
    }
}
