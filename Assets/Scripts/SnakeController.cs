using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    float horizontal;
    float vertical;

    [SerializeField] private float speed;
    public float oldspeed;
    //public float timeSinceActive = 0;
    public bool shield;
    public GameObject Top, Bottom, Left, Right;

    public Vector3 currentDirection;// = Vector3.right*Time.deltaTime;
    public Vector3 inputDirection;

    public Transform bodyprefab;
    public List<Transform> segments;// = new List<Transform>();

    private void Start()
    {
        speed = speed * Time.deltaTime;
        currentDirection = new Vector3(speed, 0, 0);

        segments = new List<Transform>();
        segments.Add(this.transform);
        Grow();
    }

    private void Update()
    {
        // taking input 
        //speed = speed * Time.deltaTime;
        /*float horizontal = (int)Input.GetAxisRaw("Horizontal") * speed;
        float vertical = (int)Input.GetAxisRaw("Vertical") * speed;*/

        horizontal = (int)Input.GetAxisRaw("Horizontal") * speed;
        vertical = (int)Input.GetAxisRaw("Vertical") * speed;

        if (horizontal != 0 || vertical != 0)
        {
            inputDirection = new Vector3(horizontal, vertical);

            if ((Mathf.Abs(inputDirection.x) != Mathf.Abs(currentDirection.x)) || (Mathf.Abs(inputDirection.y) != Mathf.Abs(currentDirection.y)))    // prevents snake from turning the opposite way instantly
            {
                currentDirection = inputDirection;
            }
        }
       // else gameObject.transform.position += currentDirection;

    }


    private void FixedUpdate()
    {

        for (int i = segments.Count - 1; i > 0; i--)
        {
            if (segments[i] != null)
                segments[i].position = segments[i - 1].position;
        }

        this.transform.Translate(currentDirection);    // moves the snake according to currentDirection 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float buffer = .9f;

        if (collision.CompareTag("++Food"))
        {
            Grow(); 
           SetSpeed(.01f);
        }

        else if (collision.CompareTag("--Food")) { Shrink(); }

        else if (collision.CompareTag("Shield")) { shield = true; Invoke("SetShieldToFalse", 7); }

        else if (collision.CompareTag("Obstacle"))
        {
            if (!shield)
            {
                Time.timeScale = 0;
                Debug.Log("Game Over Collided with Self");
            }
            else Debug.Log("Shield Power up saved you!");
        }

        else if (collision.CompareTag("Speedboost")) { SetSpeed(.7f); Invoke("SetSpeedboostToNormal",4); }  // bug- when called multiple times speed boost becomes permanent, temp implementation for now-

        else if (collision.CompareTag("Xpboost")) { }
        // WRAPPING-
        else if(collision.CompareTag("Top"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Bottom.gameObject.transform.position.y + buffer); }

        else if (collision.CompareTag("Bottom"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Top.gameObject.transform.position.y - buffer); }

        else if (collision.CompareTag("Left"))

        { this.gameObject.transform.position = new Vector2(Right.transform.position.x - buffer, this.gameObject.transform.position.y); }

        else if (collision.CompareTag("Right"))

        { this.gameObject.transform.position = new Vector2(Left.transform.position.x + buffer, this.gameObject.transform.position.y); }
    }

    void SetShieldToFalse() { shield = false; }

    void SetSpeedboostToNormal() { speed = oldspeed; Debug.Log("speed set back to normal"); }

    void SetSpeed(float speedboost) { oldspeed = speed; speed += speedboost; }
    private void Grow()
    {
        Transform segment = Instantiate(this.bodyprefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void Shrink()
    {
        if (segments.Count - 1 != 0)
            Destroy(segments[segments.Count - 1].gameObject);

        Debug.Log("destroyed " + segments[segments.Count - 1]);
    }

}
