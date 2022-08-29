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
