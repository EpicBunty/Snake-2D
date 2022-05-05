using UnityEngine;

public class Consumable : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public SnakeController snake;
    public bool Despawned;

    public int AppearAfterXsegments;
    public float ReappearAfterTime;
    public float RemainActiveTime;
    [SerializeField] private float timer = 0;

    // add a system where food dissapears after a few seconds if not eaten. consecutively eaten food without dissapearing increases score manifold.

    private void OnEnable()
    {
        RandomizeLocation();
    }

    private void FixedUpdate()
    {
        // sets it to dissapear and re-appear after set time if not consumed
        timer += Time.deltaTime;
        if (timer > RemainActiveTime) // Random.Range(RemainActiveTime - 2, RemainActiveTime + 1) ) 
        {
            timer = 0;
            DespawnConsumable();
            Invoke("SpawnConsumable", ReappearAfterTime);// Random.Range(ReappearAfterTime - 1, ReappearAfterTime + 2)); 
        }
    }
    public void SpawnConsumable()
    {
        Despawned = false;
        gameObject.SetActive(true);
    }

    public void DespawnConsumable()
    {
        Despawned = true;
        gameObject.SetActive(false);
    }

    public void RandomizeLocation()
    {

        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // func to prevent spawning on snake body
        while (snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = bounds.min.x;
                y++;

                if (y > bounds.max.y)
                {
                    y = bounds.min.y;
                }
            }
        }
        gameObject.transform.position = new Vector2(x, y);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /* if (other.CompareTag("Player"))
         {*/
        if (this.gameObject.CompareTag("Speedboost") || this.gameObject.CompareTag("Xpboost") || this.gameObject.CompareTag("Shield"))
        {
            DespawnConsumable();
            Invoke("SpawnConsumable", ReappearAfterTime);// Random.Range(ReappearAfterTime - 1, ReappearAfterTime + 2));

        }
        else
        {
            RandomizeLocation();
            timer = 0;
            //timer = RemainActiveTime;

        }

    }

}
