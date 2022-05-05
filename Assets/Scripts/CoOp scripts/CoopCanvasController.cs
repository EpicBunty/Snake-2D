using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoopCanvasController : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI player1scoreUI;
    [SerializeField] private TextMeshProUGUI player2scoreUI;
    [SerializeField] private TextMeshProUGUI playWonText;
    public GameObject GameOverMenu;
    public Button restartButton;
    public Button lobbyButton;
    public CoopSnakeController Snake;

    private void Awake()
    {
        restartButton.onClick.AddListener(ReloadScene);
        lobbyButton.onClick.AddListener(Lobby);
    }

    private int Player1score = 0;
    private int Player2score = 0;
    void Start()
    {
        RefreshScore();
    }
    public void GameOverScreen(string PlayerName)
    {
        //Time.timeScale = 0;
        OpenGameOverMenu(true);
        playWonText.gameObject.SetActive(true);
        playWonText.text = PlayerName + " WON THIS ROUND  PRESS RESTART TO TRY AGAIN";
        /* if (Player1score > Player2score)
         {
             playWonText.text = "PLAYER " + 1 + " HAS WON PRESS RESTART TO TRY AGAIN";
         }
         else playWonText.text = "PLAYER " + 2 + " HAS WON PRESS RESTART TO TRY AGAIN";*/
    }
    public void Player1ScoreIncrement(int increment)
    {
        Player1score += increment;
        RefreshScore();
    }

    public void Player2ScoreIncrement(int increment)
    {
        Player2score += increment;
        RefreshScore();
    }

    private void RefreshScore()
    {
        player1scoreUI.text = "Player 1 SCORE" + Player1score;

        player2scoreUI.text = "Player 2  SCORE" + Player2score;
    }
    public void Lobby()
    {
        //SoundManager.Instance.Play(Sounds.ButtonBack);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        Snake.ResetState();
        OpenGameOverMenu(false);
        playWonText.gameObject.SetActive(false);
        /*Snake.segments.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
    }

    public void OpenGameOverMenu(bool value)
    {
        GameOverMenu.SetActive(value);

    }
}

