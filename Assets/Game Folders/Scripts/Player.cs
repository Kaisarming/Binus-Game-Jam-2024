using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// before canvas manager script execution
[DefaultExecutionOrder(0)]
public class Player : MonoBehaviour
{
    [Header("Attributes :")] [SerializeField]
    private int healthMax;
    [SerializeField] private GameObject pfHealthBar;
    [SerializeField] private Transform pointHealthBar;
    private HealthSystem _healthSystem;
    public HealthSystem HealthSystemPlayer
    {
        get
        {
            return _healthSystem;
        }
    }
    
    // ground layer mask
    [SerializeField] private LayerMask groundLayerMask;
    
    // attack area parent
    [SerializeField] private Transform attackAreaParent;
    
    [Space(10)]

    // mechanic variable
    public float moveSpeed;

    public Rigidbody2D myRig;
    public float jump;
    public SpriteRenderer sr;

    // animation variable
    private Animator _animator;
    private int _idleHash, _walkHash, _jumpHash, _diedHash; // int hash animation

    // timer variable
    private TimerController _timerController;
    private bool _isDead;

    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            _isDead = value;
        }
    }

    private void Awake()
    {
        _timerController = FindObjectOfType<TimerController>();

        // health setup
        var newHealthBar = Instantiate(pfHealthBar, pointHealthBar.position, Quaternion.identity, transform);
        var healthBar = newHealthBar.GetComponent<HealthBar>();
        _healthSystem = new HealthSystem(healthMax);
        healthBar.Setup(_healthSystem);
        
        // add listener to health system
        _healthSystem.OnHealthChanged += HealthChanged;
    }

    private void HealthChanged(object sender, EventArgs e)
    {
        // blood runs out!
        if (_healthSystem.GetHealth() == 0)
        {
            PlayerDead(null, null);
        }
    }

    private void OnEnable()
    {
        if (_timerController != null)
        {
            _timerController.OnTimesUp += PlayerDead;
        }

        GameManager.Instance.OnGameWin += PlayerWin;
    }

    private void OnDestroy()
    {
        if (_timerController != null)
        {
            _timerController.OnTimesUp -= PlayerDead;
        }
        
        GameManager.Instance.OnGameWin -= PlayerWin;
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _idleHash = Animator.StringToHash("idle");
        _walkHash = Animator.StringToHash("walk");
        _jumpHash = Animator.StringToHash("jump");
        _diedHash = Animator.StringToHash("died");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;

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
            
            // change place attack area ahead
            attackAreaParent.localScale = new Vector3(1, 1, 1);
        }
        else if (myRig.velocity.x < 0)
        {
            sr.flipX = false;

            // change place attack area ahead
            attackAreaParent.localScale = new Vector3(-1, 1, 1);
        }

        PlayAnimation();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        float xInput = Input.GetAxisRaw("Horizontal");
        // Velocity is the direction and speed  that the rigidbody is moving in
        myRig.velocity = new Vector2(xInput * moveSpeed, myRig.velocity.y);
    }

    private void PlayAnimation()
    {
        if (myRig.velocity.y > 0.01f)
        {
            // play animation jump
            if (_animator.GetCurrentAnimatorStateInfo(0).tagHash == _jumpHash) return;
            _animator.Play(_jumpHash);
        }
        else if (Mathf.Abs(myRig.velocity.x) > 0.01f && IsOnTheGround())
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


    private bool IsOnTheGround()
    {
        // A raycast shot from an origin in a direction for a certain distance, that means it can tell us if it hits anything
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -1f, 0),
            Vector2.down, 0.2f, groundLayerMask);
        return hit.collider != null;
    }

    private void PlayerDead(object sender, EventArgs e)
    {
        _animator.Play(_diedHash);
        StopPlayer();
        
        // game lose
        GameManager.Instance.ChangeState(Gamestate.GameOver);
    }

    public void PlayerWin(object sender, EventArgs e)
    {
        // play animation win if there
        //....
        // stop
        StopPlayer();
    }
    
    private void StopPlayer()
    {
        _isDead = true;
        myRig.velocity = Vector2.zero;
        Destroy(myRig, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _isDead = true;
            
            GameManager.Instance.ChangeState(Gamestate.Result);
        }

        if (collision.CompareTag("Fall"))
        {
            _isDead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            // no code
        }
    }

    // We make GameOver() to public because we want to call it in Enemy.cs function called "private void OnCollisionEnter2D(Collision2D other)"
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}