using System;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    
    private float vertical;
    private bool isLadder;
    private bool isClimbing;

    private Rigidbody2D rb;
    private bool gameOver = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        GameManager.Instance.OnGameWin += EndGame;
        GameManager.Instance.OnGameLose += EndGame;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameWin -= EndGame;
        GameManager.Instance.OnGameLose -= EndGame;
    }

    private void EndGame(object sender, EventArgs e)
    {
        gameOver = true;
    }

    void Update()
    {
        if (gameOver) return;
        
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (gameOver) return;
        
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}