using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ballMovement : MonoBehaviour
{
    [SerializeField] private float initalSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI AIScore;

    public AudioSource audioSource;

    private int hitCounter;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initalSpeed + (speedIncrease + hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1,0) * (initalSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0,0);
        transform.position = new Vector2(0,0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObj)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 PlayerPos = myObj.position;

        float xDirection, yDirection;

        if(transform.position.x  > 0)
        {
            xDirection = -1;
        }

        else
        {
            xDirection = 1;
        }

        yDirection = (ballPos.y - PlayerPos.y) / myObj.GetComponent<Collider2D>().bounds.size.y;

        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initalSpeed + (speedIncrease * hitCounter));



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            ResetBall();
            playerScore.text = (int.Parse(playerScore.text)+ 1).ToString();
        }

        else if(collision.CompareTag("AIGoal"))
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }

}
