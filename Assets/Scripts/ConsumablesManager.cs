using UnityEngine;

public class ConsumablesManager : MonoBehaviour
{
    public SnakeController snake; //check snake segments and spawn food accordingly
    public int SnakeSegments;
    public Consumable[] consumables;
    
    void Update()
    {
        int snakeSegmentsCount = snake.segments.Count;
        // find more performant way of doing this
        for (int i = 0; i < consumables.Length; i++)
        {
            if (snakeSegmentsCount >= consumables[i].AppearAfterXsegments && !consumables[i].Despawned)
            {
                consumables[i].gameObject.SetActive(true);
            }
            else consumables[i].gameObject.SetActive(false);
        }
    }

}
