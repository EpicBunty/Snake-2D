using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UITextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI levelindicatorUI;

    private int score = 0;
    private int level;// = LevelManager.Instance.CurrentSceneIndex;
    /*private void Awake()
    {
        scoreUI = gameObject.GetComponent<TextMeshProUGUI>();
    }*/

    void Start()
    {
        RefreshScore();
        RefreshLevelIndicator();
    }

    public void ScoreIncrement(int increment)
    {
        score += increment;
        RefreshScore();
    }

    private void RefreshScore()
    {
        scoreUI.text = "Score: " + score;
    }

    private void RefreshLevelIndicator()
    {
        level = SceneManager.GetActiveScene().buildIndex;// +1;
        levelindicatorUI.text = "Level : " + level;
    }
}