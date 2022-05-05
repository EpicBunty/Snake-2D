using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUIController : MonoBehaviour
{
    public bool scoreBoost=false;
    [SerializeField] private TextMeshProUGUI scoreUI;
    public GameObject GameOverMenu;
    public Button restartButton;
    public Button mainMenuButton;
    public SnakeController Snake;

    private void Awake()
    {
        restartButton.onClick.AddListener(ReloadScene);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private int score = 0;
    //private int level;
    void Start()
    {
        RefreshScore();
    }

    public void ScoreIncrement(int increment)
    {
        if (scoreBoost)
        { increment *= 2; }

        score += increment; 
        RefreshScore();
    }

    private void RefreshScore()
    {
        scoreUI.text = "SCORE" + score;

    }
    public void MainMenu()
    {
        //SoundManager.Instance.Play(Sounds.ButtonBack);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ReloadScene()
    {
        Snake.ResetState();
        OpenGameOverMenu(false);
        /*Snake.segments.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
    }

    public void OpenGameOverMenu(bool value)
    {
        GameOverMenu.SetActive(value);
        /*if (value==true)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;*/
    }
}