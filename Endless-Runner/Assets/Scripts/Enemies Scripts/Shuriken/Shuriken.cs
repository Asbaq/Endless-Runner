using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
   private bool moveAndAttack;
   private bool attackRight;
   private float attackspeed=6f;
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,Random.Range(0f,-45f)));
        moveAndAttack = true;
        if(transform.position.x > 0)
        {
          attackRight =false;    
        }
        else
        {
          attackRight = true;
        }
           
        Invoke("Deactivate",5f);
    }

    void Update()
    {
        shurikans();
    }

    void shurikans()
    {
        if(moveAndAttack)
        {
            if(!attackRight)
            {
                transform.position -= Vector3.right * attackspeed * Time.deltaTime; 
            }
            else
            {
                transform.position += Vector3.right * attackspeed * Time.deltaTime;
            }
        }
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
