using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Disparo117 : MonoBehaviour
{
    public BulletData[] bulletData;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private GameObject[] typesBullets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigos") && transform.CompareTag("BulletPlayer"))
        {
            other.GetComponent<ControladorDeEnemigos>().RecibirDanio(bulletData[0].BulletDamage);
            other.GetComponent<ControladorDeEnemigos>().cronometro = 0.3f;
            ResetProps();
        }

        if (other.CompareTag("Player") && transform.CompareTag("BulletEnemy"))
        {
            other.GetComponent<Player117>().RecibirDanio(bulletData[1].BulletDamage);
            ResetProps();
        }

        if (other.CompareTag("Paredes"))
        {
            ResetProps();
        }
    }
    public void ResetProps()
    {
        gameObject.tag = "Untagged";
        gameObject.transform.rotation = Quaternion.identity;
        //Debug.Log(gameObject.transform.position);
        gameObject.SetActive(false);
    }
    public void SetProps(Vector2 _vel, Vector2 _pos, float _ang, BulletData _bulletData)
    {
        bulletData[1] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[1].Sprite;
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[1].Animator;
        if (bulletData[1].TagName=="BulletBoss")
        {
            gameObject.transform.localScale = Vector3.one * 4;

        }
        else
        {
            gameObject.transform.localScale = Vector3.one * 0.5f;
        }

        gameObject.tag = bulletData[1].TagName;
        transform.rotation = Quaternion.Euler(0, 0, _ang);
        transform.position = _pos;
        bulletRB.velocity = _vel;
    }

    public void SetPropsPlayer(Vector2 _pos, BulletData _bulletData)
    {
        bulletData[0] = _bulletData;
        gameObject.GetComponent<SpriteRenderer>().sprite = bulletData[0].Sprite;
        gameObject.transform.localScale = Vector3.one*3;
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletData[0].Animator;
        gameObject.tag = bulletData[0].TagName;
        transform.position = _pos;
    }
}

