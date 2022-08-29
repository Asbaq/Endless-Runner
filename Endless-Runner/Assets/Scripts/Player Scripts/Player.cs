using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private Animator anim;
    private bool onLeft,onRight;
    private bool jumped;

    [SerializeField]
    AudioSource audioKill,audioJump;

    [SerializeField]
    private AudioClip deadSound;
    private bool isAlive = true;
   
    void Awake()
    {
       // GameObject.Find("JumpButton").GetComponent<Button>().onClick.AddListener(Jump);
        anim=GetComponent<Animator> ();
        onRight = true;
      //  onLeft  = false;
    }

    void Update()
    {
        if(isAlive)
        {
            if(!jumped)
        {
        if(onRight)
        {
            anim.Play("Run Right Animation");
        }   
        else
        {
            anim.Play("RunLeft Animation");
        }
        }

            //Jump();
        // if(Input.GetKeyDown (KeyCode.Space))
        // {
        //  if(onRight)
        // {
        //     anim.Play("JumpLeft Animation");
        // }   
        // else
        // {
        //     anim.Play("Jump Right Animation");
        // }
        // jumped=true;   
        // audioJump.Play();
        // }
        }
    }

    public void Jump()
    {
        if(isAlive)
        {
            if(onRight)
        {
            anim.Play("JumpLeft Animation");
        }   
        else
        {
            anim.Play("Jump Right Animation");
        }
        jumped=true;
        audioJump.Play();
        }     
    }

    void OnLeft()
    {
        onLeft=true;
        onRight=false;    
        jumped =false;
    }
    
    void OnRight()
    {
        onLeft=false;
        onRight=true;
        jumped =false;
    }

    void PlayerDied()
    {
        audioKill.clip = deadSound;
        audioKill.Play();
        isAlive = false;

        if(transform.position.x > 0)
        {
            anim.Play("PlayerDeidRight");
        }
        else
        {
            anim.Play("PlayerDeidLeft");
        }

        GameController.instance.GameOver();

        Time.timeScale = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        {
            if(jumped)
            {
                if(other.tag == "Enemies")
                {
                    other.gameObject.SetActive(false);
                    audioKill.Play();
                }
                else
                {
                    if(other.tag == "Enemy Tree")
                    {
                    PlayerDied();
                    }
                }
            }
        }
    }
} 