using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 6f;
    [SerializeField] private Rigidbody2D laserRB;

    private void OnEnable()
    {
        laserRB.velocity = Vector2.up * laserSpeed;
    }

    private void OnCollisionEnter2D()
    {
        gameObject.SetActive(false);
    }
}