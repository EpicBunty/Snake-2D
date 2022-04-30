/*using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D boundary;
    public int eatenCount = 0;
    public GameObject foodPrefab;
    bool ScoreBoost;
    bool FoodSpawned = true;
    public bool Eaten = false;
    public float timer = 7;
    public float timesinceawake = 0;

    // add a system where food dissapears after a few seconds if not eaten. consecutively eaten food without dissapearing increases score manifold.

    private void Awake()
    {
        RandomizeLocation();
    }

    private void Update()
    {
        timesinceawake = timesinceawake + Time.deltaTime;

        if (timesinceawake >= timer)
        {
            if (!Eaten)
            {
                Destroy(gameObject);
            }
            else Eaten = true;
        }

        // if food is not spawned 
        *//* if (!FoodSpawned)
             Instantiate(foodPrefab);*//*
    }
    private void FoodEaten()
    {
        Eaten = true;
        // increment score count or multiply by 2 if boost is active
        eatenCount += 1;
        if (ScoreBoost)
        { eatenCount *= 2; }
        // set the transform to a random location
        RandomizeLocation();
    }

    private void RandomizeLocation()
    {
        // randomize location
        Bounds bounds = boundary.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        gameObject.transform.position = new Vector2(x, y);
    }

   *//* void DestroyFood()
    {
        Debug.Log("Destroying Food");
        FoodSpawned = false;
        Destroy(this.gameObject);
    }*/

    /*void EnableFood()
    {
        Debug.Log("Enabling Food");
        FoodSpawned = true;
        gameObject.SetActive(true);
    }*//*

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            FoodEaten();
        }
    }


}
*/