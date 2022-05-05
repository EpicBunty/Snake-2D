using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    [SerializeField] private float speed;
    public int initialSize;

    [HideInInspector] public bool shieldBoost;
    /*[HideInInspector]*/
    public bool speedBoost;
    public GameObject Top, Bottom, Left, Right;

    public CanvasUIController UIcontroller;

    public Vector3 currentDirection;
    public Vector3 inputDirection;

    public Transform bodyprefab;
    public List<Transform> segments= new List<Transform>();

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        InputBasedOnPlayer();

        if (horizontal != 0 || vertical != 0)
        {
            inputDirection = new Vector3(horizontal * speed, vertical * speed);

            if ((Mathf.Abs(inputDirection.x) != Mathf.Abs(currentDirection.x)) || (Mathf.Abs(inputDirection.y) != Mathf.Abs(currentDirection.y)))    // prevents snake from turning the opposite way instantly
            {
                currentDirection = inputDirection;
            }
        }

        OpenInGameMenu();
    }

    private void OpenInGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIcontroller.GameOverMenu.activeInHierarchy)

            { UIcontroller.OpenGameOverMenu(false); }

            else { UIcontroller.OpenGameOverMenu(true); }
        }
    }

    private void InputBasedOnPlayer()
    {
        if (this.CompareTag("Player"))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                vertical = 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                vertical = -1;
            }
            else vertical = 0;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                horizontal = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                horizontal = -1;
            }
            else horizontal = 0;
        }
    }

    private void FixedUpdate()
    {

        for (int i = segments.Count - 1; i > 0; i--)
        {
            if (segments[i] != null)
                segments[i].position = segments[i - 1].position;
        }

        transform.Translate(currentDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float buffer = 1.1f;

        if (collision.CompareTag("++Food"))
        {
            Grow();
            speed += .01f;    // ADJUST
            UIcontroller.ScoreIncrement(1);
        }

        else if (collision.CompareTag("--Food"))
        {
            Shrink();
            UIcontroller.ScoreIncrement(1);
        }

        else if (collision.CompareTag("Xpboost"))
        {
            UIcontroller.scoreBoost = true;
            Invoke("SetScoreBoostToFalse", 7);
        }

        else if (collision.CompareTag("Shield"))
        {
            shieldBoost = true;
            Invoke("SetShieldToFalse", 7);
        }

        else if (collision.CompareTag("Player1body"))
        {
            if (!shieldBoost)
            {
                Debug.Log("Game Over Collided with Self");
                ResetState();
            }
            else Debug.Log("Shield Power up saved you!"); // change later
        }

        else if (collision.CompareTag("Speedboost"))
        {
            speedBoost = true;
            SpeedBoost();
            Invoke("SetSpeedboostToFalse", 4); // bug- when called multiple times speed boost becomes permanent, temp implementation for now-
        }

        // WRAPPING-
        else if (collision.CompareTag("Top"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Bottom.gameObject.transform.position.y + buffer); }

        else if (collision.CompareTag("Bottom"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Top.gameObject.transform.position.y - buffer); }

        else if (collision.CompareTag("Left"))

        { this.gameObject.transform.position = new Vector2(Right.transform.position.x - buffer, this.gameObject.transform.position.y); }

        else if (collision.CompareTag("Right"))

        { this.gameObject.transform.position = new Vector2(Left.transform.position.x + buffer, this.gameObject.transform.position.y); }
    }

    void SetShieldToFalse() { shieldBoost = false; }

    void SetScoreBoostToFalse() { UIcontroller.scoreBoost = false; }

    void SetSpeedboostToFalse() { speedBoost = false; SpeedBoost(); }

    void SpeedBoost()
    {
        if (speedBoost)
        {
            speed += .3f;     //ADJUST THIS VALUE
        }
        else speed -= .3f;

        currentDirection.x *= speed;
        currentDirection.y *= speed;
    }
    //void SetSpeed(float speedboost) { oldspeed = speed; speed += speedboost; }
    private void Grow()
    {
        Transform segment = Instantiate(this.bodyprefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void Shrink()
    {
        if (segments.Count > 1)
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.RemoveAt(segments.Count - 1);
        }

        Debug.Log("destroyed " + segments[segments.Count - 1].name);
    }


    // function to prevent stuff spawning on the snake body
    public bool Occupies(float x, float y)
    {
        foreach (Transform segment in segments)
        {
            if (segment.position.x == x && segment.position.y == y)
            {
                return true;
            }
        }

        return false;
    }

    public void ResetState()
    {
        transform.position = Vector3.zero;
        currentDirection = new Vector3(speed, 0, 0);
        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);
        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }
}
