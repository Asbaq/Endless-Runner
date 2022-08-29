using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    public float movespeed = 3f;
        void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += movespeed * Time.deltaTime;
        transform.position = temp;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag=="sideBound")
        {
            movespeed *= -1f;

            Vector3 t = transform.localScale;
            t.x *= -1f;
            transform.localScale = t; 
        }
    }
}
