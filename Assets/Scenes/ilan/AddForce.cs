using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float degree;

    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        AddingForce();
    }

    private void AddingForce()
    {
        float calculatedDegree = degree / 360 * 2 * Mathf.PI;
        float calculatedSin = Mathf.Sin(calculatedDegree);
        Debug.Log(calculatedSin.ToString());
        Vector2 directionToShoot = new Vector2(Mathf.Cos(calculatedDegree), Mathf.Sin(calculatedDegree));
        rb2d.AddForce(force * directionToShoot);
    }

}
