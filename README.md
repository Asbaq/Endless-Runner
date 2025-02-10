# Endless Runner 🏃‍♂️💨

![Endless Runner](https://user-images.githubusercontent.com/62818241/201542177-b11a436c-9c1d-42a1-a62c-38370d5bd40c.png)

## 📌 Introduction
**Endless Runner** is an exciting 2D side-scrolling game where the player controls a character who continuously moves forward while dodging enemies and obstacles. The goal is to survive as long as possible while defeating enemies, avoiding traps, and collecting power-ups. The game features smooth animations, a progression system, enemy spawning, and a dynamic scoring system.

🔗 Video Trailer

https://youtube.com/shorts/oep3hfKN5co?si=aR5vu0bQQQnX5iCu

## 🔥 Features
- 🏃‍♂️ **Character Movement**: The player character moves forward automatically, with the option to jump and dodge.
- 🧟‍♂️ **Enemy Encounters**: Various enemies, like bees and ninjas, spawn randomly, each with their own unique attack behaviors.
- 💥 **Combat Mechanics**: The player can defeat enemies by jumping on them or dodging their attacks.
- 🔄 **Procedural Level Generation**: The game continuously generates new levels, creating an endless experience.
- 📊 **Scoring System**: A dynamic score system based on the character's survival time and enemy eliminations.
- 🎶 **Sound Effects**: Interactive sound effects for jumping, defeating enemies, and background music.
- 🎮 **Game Over and Restart**: When the player dies, a Game Over screen is displayed with an option to retry.

---

## 🏗️ How It Works

This game is controlled through simple interactions like jumping and moving left or right. The character's movement is automatic, while players can control jumping and dodging actions. The game features automatic enemy spawning, smooth animations, and a scoring system that tracks progress.

### 📌 **BGMover Script**

The **BGMover** script handles the movement of the background elements and the spawning of enemies at random intervals.

```csharp
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
                    highestBoundsY = sideBounds [i].transform.position.y;
                }
            }

            Vector3 temp =transform.position;
            temp.y = highestBoundsY + boundHeight - 1f;
            transform.position = temp;

            Spwanner();
        }
    }

    void Spwanner()
    {
        if(Random.Range(0,10)>0)
        {
            int randomEnemyIndex = Random.Range(0, enemies.Length);

            if(randomEnemyIndex == 0)
            {
                Instantiate(enemies[randomEnemyIndex], new Vector3(0f, transform.position.y, 3f), Quaternion.identity);
            }
            else
            {
                GameObject Enemyobj = Instantiate(enemies[randomEnemyIndex]);
                Vector3 EnemyScale = Enemyobj.transform.localScale;

                if(Random.Range(0, 2) > 0)
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
```

### 📌 **GameController Script**

The **GameController** script handles the game's score system and manages the game's state (Game Over, Restart, etc.).

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private Text scoretext;
    private int score;
    public Text endscore;
    public GameObject scorePanel;
    public Animator endPanelAnim;

    void Awake()
    {
        MakeInstace();    
    }

    void Start()
    {
        scoretext = GameObject.Find("ScoreText").GetComponent<Text>();
        StartCoroutine (CountScore());
    }

    void MakeInstace()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    IEnumerator CountScore()
    {
        yield return new WaitForSeconds(0.1f);
        score++;
        scoretext.text = score.ToString();
        StartCoroutine (CountScore());
    }

    public void GameOver()
    {
        scorePanel.SetActive(false);
        endscore.text="Height : " + score;
        endPanelAnim.Play("EndPanel");
    }

    public void Again()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
```

### 📌 **Player Script**

The **Player** script handles the character's movement and interactions with enemies and obstacles, including jumping.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private Animator anim;
    private bool onLeft, onRight;
    private bool jumped;

    [SerializeField]
    AudioSource audioKill, audioJump;

    [SerializeField]
    private AudioClip deadSound;
    private bool isAlive = true;
   
    void Awake()
    {
        anim = GetComponent<Animator>();
        onRight = true;
    }

    void Update()
    {
        if (isAlive)
        {
            if (!jumped)
            {
                if (onRight)
                    anim.Play("Run Right Animation");
                else
                    anim.Play("Run Left Animation");
            }
        }
    }

    public void Jump()
    {
        if (isAlive)
        {
            if (onRight)
                anim.Play("JumpLeft Animation");
            else
                anim.Play("Jump Right Animation");
            jumped = true;
            audioJump.Play();
        }
    }

    void PlayerDied()
    {
        audioKill.clip = deadSound;
        audioKill.Play();
        isAlive = false;

        if (transform.position.x > 0)
            anim.Play("PlayerDeadRight");
        else
            anim.Play("PlayerDeadLeft");

        GameController.instance.GameOver();
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (jumped)
        {
            if (other.tag == "Enemies")
            {
                other.gameObject.SetActive(false);
                audioKill.Play();
            }
            else if (other.tag == "EnemyTree")
            {
                PlayerDied();
            }
        }
    }
}
```

---

## 🎯 Conclusion

The **Endless Runner** game offers a fun and challenging experience with automatic movement, enemy encounters, and smooth animations. The game mechanics are designed for players to engage in a fast-paced adventure while attempting to beat their score. With the addition of enemy spawning, combat mechanics, and a scoring system, this project provides an excellent introduction to Unity development and game mechanics.
