using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    public GameObject Shuriken;
    public float movespeed = 4f;

    private float cameraY;
    private bool attackStarted;
    void Start()
    {
        cameraY = Camera.main.transform.position.y - 10f;
    }

    void Update()
    {
        NinjaEnemy();
        Deactivate();
    }

    void NinjaEnemy()
    {
            Vector3 temp = transform.position;
            temp.y -= movespeed * Time.deltaTime;
            transform.position = temp;

            if(transform.position.y < 0)
            {
            if(!attackStarted)
            {
            attackStarted = true;
            Instantiate(Shuriken,transform.position,Quaternion.identity);
            }
            }
        
    }

    void Deactivate()
    {
        if(transform.position.y < cameraY)
        {
            gameObject.SetActive(false);
        }
    }
}

