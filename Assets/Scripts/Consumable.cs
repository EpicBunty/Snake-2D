using UnityEngine;

public class Consumable : MonoBehaviour
{
    public BoxCollider2D gridArea;
    //public GameObject Prefab;
    public float timetoRemainActive;
    [SerializeField] private float timeSinceActive = 0;

    // add a system where food dissapears after a few seconds if not eaten. consecutively eaten food without dissapearing increases score manifold.

    private void OnEnable()
    {
        RandomizeLocation();
    }

    private void FixedUpdate()
    {
        timeSinceActive += Time.deltaTime;

        if (timeSinceActive > timetoRemainActive)
        {
            gameObject.SetActive(false);
            timeSinceActive = 0;
        }
    }

    public void RandomizeLocation()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        gameObject.transform.position = new Vector2(x, y);

        timeSinceActive = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            RandomizeLocation();
        }
    }

}
