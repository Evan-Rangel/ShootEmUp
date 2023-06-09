using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPool : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField, Range(0, 50)] private int poolSize;
    [SerializeField] private List<GameObject> laserList;
    private static ShotPool instance;
    public static ShotPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }
    void Start()
    {
        AddLaserToPool(poolSize);
    }

    private void AddLaserToPool(int amount)
    {
        GameObject laser = Instantiate(laserPrefab);
        laser.SetActive(false);
        laserList.Add(laser);
        laser.transform.parent = transform;
    }

    public GameObject RequestLaser()
    {
        for (int i = 0; i < laserList.Count; i++)
        {
            if (!laserList[i].activeSelf)
            {
                laserList[i].SetActive(true);
                return laserList[i];
            }
        }
        AddLaserToPool(1);
        laserList[laserList.Count - 1].SetActive(true);
        return laserList[laserList.Count - 1];
    }

}
