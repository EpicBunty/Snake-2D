using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePowerUp : MonoBehaviour
{
    [SerializeField] private SnakeController Snake;
    
    [SerializeField] private Image scoreBoost;
    [SerializeField] private Image speedBoost;
    [SerializeField] private Image shieldBoost;
    
    void Update()
    {
        scoreBoost.gameObject.SetActive(Snake.UIcontroller.scoreBoost);
        speedBoost.gameObject.SetActive(Snake.speedBoost);
        shieldBoost.gameObject.SetActive(Snake.shieldBoost);
    }
}
