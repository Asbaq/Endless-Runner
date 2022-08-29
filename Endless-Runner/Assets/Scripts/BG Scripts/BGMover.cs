using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
   public float moveSpeed = 4f;
   private GameObject[] sideBounds;
   private float CameraY;
   private float boundHeight;
   public GameObject[] enemies;
   public GameObject[] spawnPosition;
    void Awake()
    {
        sideBounds = GameObject.FindGameObjectsWithTag("sideBound");
        CameraY = Camera.main.gameObject.transform.position.y - 15f;
        boundHeight = GetComponent<BoxCollider2D>().bounds.size.y;        
    }

    void Update()
    {
        Move();
        Reposition();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y -= moveSpeed * Time.deltaTime;
        transform.position = temp;
    }
  void Reposition()
    {
        if(transform.position.y < CameraY)
        {
            float highestBoundsY = sideBounds [0].transform.position.y;
            for(int i=1; i < sideBounds.Length; i++)
            {
                if(highestBoundsY < sideBounds [i].transform.position.y)
                {
                    highestBoundsY=sideBounds [i].transform.position.y;
                }
            }

            Vector3 temp =transform.position;
            temp.y=highestBoundsY + boundHeight - 1f;
            transform.position=temp;

            Spwanner();
       }
    }

    void Spwanner()
    {
        if(Random.Range(0,10)>0)
        {
            int randomEnemyIndex = Random.Range(0,enemies.Length);

            if(randomEnemyIndex == 0)
            {
                Instantiate(enemies[randomEnemyIndex],new Vector3(0f,transform.position.y,3f),Quaternion.identity);
            }
            else
            {
                GameObject Enemyobj = Instantiate(enemies[randomEnemyIndex]);
                Vector3 EnemyScale = Enemyobj.transform.localScale;

                if(Random.Range(0,2)>0)
                {
                    Enemyobj.transform.position = spawnPosition[0].transform.position;
                    EnemyScale.x = Mathf.Abs(EnemyScale.x);
                }
                else
                {
                    Enemyobj.transform.position = spawnPosition[1].transform.position;
                    EnemyScale.x = -Mathf.Abs(EnemyScale.x);
                }    
                Enemyobj.transform.localScale = EnemyScale; 
            }
        }
    }
}