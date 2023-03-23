using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Disparo117 : MonoBehaviour
{
    public BulletData bulletData;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private GameObject[] typesBullets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*switch (other.tag)
        {
            case "Enemigos":
                break;
            case "Paredes":
                break;
            default:
                break;
        }*/
        
         if (other.CompareTag("Enemigos") && transform.CompareTag("BulletPlayer"))
        {
            other.GetComponent<EnemyHealth>().RecibirDanio(bulletData.BulletDamage);
            ResetProps();
        }
        
        if (other.CompareTag("Paredes"))
        {
            ResetProps();
        }
    }
    void ResetProps()
    {
        gameObject.tag = "Untagged";
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        //bulletRB.velocity = Vector2.zero;
        gameObject.transform.rotation = Quaternion.identity;
        Debug.Log(gameObject.transform.position);
        gameObject.SetActive(false);
    }
    public void SetProps(Vector2 _vel, Vector2 _pos, float _ang, BulletData _bulletData)
    {
        bulletData = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData.Sprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData.Animator;
        gameObject.tag = bulletData.TagName;

        transform.rotation = Quaternion.Euler(0,0,_ang);
        transform.position = _pos;
        bulletRB.velocity = _vel;
    }
}
