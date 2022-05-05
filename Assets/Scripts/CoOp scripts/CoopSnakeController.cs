using System.Collections.Generic;
using UnityEngine;

public class CoopSnakeController : MonoBehaviour
{
    [SerializeField] private float speed;
    public float horizontal;
    public float vertical;

    public int initialSize;

    public GameObject Top, Bottom, Left, Right;

    public CoopCanvasController CoopUIcontroller;

    public Vector3 currentDirection;
    public Vector3 inputDirection;

    public string Playertag;
    public string Enemytag;

    public Transform bodyprefab;
    //public Transform bodyprefabp2;
    public List<Transform> segments = new List<Transform>();

    private void Start()
    {
        SoundManager.Instance.PlayBgMusic(Sounds.FightMusic);
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
            CoopUIcontroller.OpenGameOverMenu(true);
        }
        
    }

    private void InputBasedOnPlayer()
    {
        if (CompareTag("Player1"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                vertical = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                vertical = -1;
            }
            else vertical = 0;

            if (Input.GetKeyDown(KeyCode.D))
            {
                horizontal = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                horizontal = -1;
            }
            else horizontal = 0;
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

        if (collision.CompareTag("Top"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Bottom.gameObject.transform.position.y + buffer); }

        else if (collision.CompareTag("Bottom"))

        { this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, Top.gameObject.transform.position.y - buffer); }

        else if (collision.CompareTag("Left"))

        { this.gameObject.transform.position = new Vector2(Right.transform.position.x - buffer, this.gameObject.transform.position.y); }

        else if (collision.CompareTag("Right"))

        { this.gameObject.transform.position = new Vector2(Left.transform.position.x + buffer, this.gameObject.transform.position.y); }

        /*if (this.CompareTag(Playertag) && collision.CompareTag(Enemytag+"body") )
        {
            CoopUIcontroller.PlayerScoreIncrement(1, Playertag + "score");
            Debug.Log(Playertag + "score");
        }*/
        if (this.CompareTag("Player1") && collision.CompareTag("Player2body"))
        {
            CoopUIcontroller.Player1ScoreIncrement(1);
            CoopUIcontroller.GameOverScreen("Player 1");
            initialSize += 1;
        }
        else if (this.CompareTag("Player2") && collision.CompareTag("Player1body"))
        {
            CoopUIcontroller.Player2ScoreIncrement(1);
            CoopUIcontroller.GameOverScreen("Player 2");
            initialSize += 1;
        }

    }

   /* public void GameOver()
    {
        CoopUIcontroller.GameOverScreen();
    }*/
    private void Grow()
    {/*
        if (gameObject.CompareTag("Player1"))
        {*/
            Transform segment = Instantiate(this.bodyprefab);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        /*}
        else
        {
            Transform segment = Instantiate(this.bodyprefabp2);
            segment.position = segments[segments.Count - 1].position;
            segments.Add(segment);
        }*/
    }

    public void ResetState()
    {
        if (gameObject.CompareTag("Player1"))
        {
            transform.position = new Vector3(-17, 0);
        }
        else
        {
            transform.position = new Vector3(17, 0);
        }
        currentDirection = new Vector3(0, speed, 0);
        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(this.transform);
        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }
}