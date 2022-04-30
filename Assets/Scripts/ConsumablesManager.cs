using UnityEngine;

public class ConsumablesManager : MonoBehaviour
{
    public SnakeController snakeController; //check snake segments and spawn food accordingly

    public Consumable foodConsumable;
    public Consumable massGainerFood;
    public Consumable massLoserFood;
    public Consumable shieldPower;
    //public Consumable speedBoost;
    public float enableAfterTime;
    void Start()
    {

    }

    void Update()
    {
        if (!foodConsumable.gameObject.activeSelf)
            Invoke("EnableConsumable", enableAfterTime);
    }

    void EnableConsumable()
    {
        foodConsumable.gameObject.SetActive(true);
    }

    /* void DisableConsumable()
     {
         timeSinceActive += Time.deltaTime;

         if (timeSinceActive >= timetoRemainActive)
         {
             Destroy(gameObject);
         }
     }*/
}
