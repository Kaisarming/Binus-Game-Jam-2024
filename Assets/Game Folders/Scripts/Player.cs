using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D myRig;
    public float jump;
    public SpriteRenderer sr;

    private Animator _animator;
    private int _idleHash, _walkHash, _jumpHash; // int hash animation

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _idleHash = Animator.StringToHash("idle");
        _walkHash = Animator.StringToHash("walk");
        _jumpHash = Animator.StringToHash("jump");
    }

    // Update is called once per frame
    void Update()
    {
        // KeyCode = True on the frame that the up arrow is pressed down
        if (Input.GetKeyDown(KeyCode.W) && IsOnTheGround())
        {
            // AddForce = Components of the force in the X and Y axis
            // ForceMode2D = Gradual force increase
            // Impulse = Instant velocity (Add an instant force impulse to the rigidbody, using its mass.)
            // Structure = AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
            // AddForce can also be used by Vector2
            myRig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        if (myRig.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else if (myRig.velocity.x < 0)
        {
            sr.flipX = false;
        }

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (myRig.velocity.y > 0.01f)
        {
            // play animation jump
            if (_animator.GetCurrentAnimatorStateInfo(0).tagHash == _jumpHash) return;
            _animator.Play(_jumpHash);
        } else if (Mathf.Abs(myRig.velocity.x) > 0.01f && IsOnTheGround())
        {
            // play animation walk
            if (_animator.GetCurrentAnimatorStateInfo(0).tagHash == _walkHash) return;
            _animator.Play(_walkHash);
        }
        else if (Mathf.Abs(myRig.velocity.x) <= 0.01f && Mathf.Abs(myRig.velocity.y) <= 0.01f && IsOnTheGround())
        {
            // play animation idle
            if (_animator.GetCurrentAnimatorStateInfo(0).tagHash == _idleHash) return;
            _animator.Play(_idleHash);
        }
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        // Velocity is the direction and speed  that the rigidbody is moving in
        myRig.velocity = new Vector2(xInput * moveSpeed, myRig.velocity.y);
    }

    bool IsOnTheGround()
    {
        // A raycast shot from an origin in a direction for a certain distance, that means it can tell us if it hits anything
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -1f, 0), Vector2.down, 0.2f);
        return hit.collider != null;
    }

    // We make GameOver() to public because we want to call it in Enemy.cs function called "private void OnCollisionEnter2D(Collision2D other)"
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}