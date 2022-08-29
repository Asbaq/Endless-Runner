using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
   public float movespeed = 4f;
   private float Camera_Y;
    void Start()
    {
        Camera_Y = Camera.main.transform.position.y -10f;
    }

   
   void Update()
    {
        Move();
        Deactivate();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y -= movespeed * Time.deltaTime;
        transform.position = temp;
    }

    void Deactivate()
    {
        if(transform.position.y < Camera_Y)
        {
            gameObject.SetActive(false);
        }
    }
}
