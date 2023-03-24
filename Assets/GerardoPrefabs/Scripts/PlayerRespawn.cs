using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{

    public GameObject[] baterias;
    public int life;
    
    void Start()
    {
        life = baterias.Length;
    }

    void CheckLife()
    {
        if (life < 1)
        {
            //Destroy(baterias[0].gameObject);
            baterias[0].SetActive(false);
            PlayerDead();
        }
        else if (life < 2)
        {
            //Destroy(baterias[1].gameObject);
            baterias[1].SetActive(false);
            baterias[0].SetActive(true);
        }
        else if (life < 3)
        {
            //Destroy(baterias[2].gameObject);
            baterias[2].SetActive(false);
            baterias[1].SetActive(true);
        }        
        else if (life == 3)
        {
            baterias[2].SetActive(true);
            baterias[1].SetActive(true);
        }
    }

    public void PlayerDamaged()
    {
        life--;
        CheckLife();
        Debug.Log(life);
    }    
    public void PlayerHealthed()
    {
        life++;

        if (life >= baterias.Length)
        {
            life = baterias.Length;
        }

        CheckLife();
    }

    public void PlayerDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
