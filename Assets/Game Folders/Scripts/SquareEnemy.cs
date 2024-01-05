using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : MonoBehaviour
{
    [Header("Attributes :")] [SerializeField]
    private int healthMax;

    [SerializeField] private GameObject pfHealthBar;
    [SerializeField] private Transform pointHealthBar;
    private HealthSystem _healthSystem;
    public HealthSystem HealthSystemEnemy
    {
        get
        {
            return _healthSystem;
        }
    }
    
    [Space(10)]
    
    private Vector3 startPosition;
    public Vector3 targetPosition;
    public float moveSpeed;
    private bool movingTowardTargetPosition;

    private void Awake()
    {
        // health setup
        var newHealthBar = Instantiate(pfHealthBar, pointHealthBar.position, Quaternion.identity, transform);
        var healthBar = newHealthBar.GetComponent<HealthBar>();
        _healthSystem = new HealthSystem(healthMax);
        healthBar.Setup(_healthSystem);
        
        // add listener to health system
        _healthSystem.OnHealthChanged += HealthChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        movingTowardTargetPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTowardTargetPosition == true)
        {
            //MoveTowards formula (Same for Vector2) = Vector3.MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                movingTowardTargetPosition = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                movingTowardTargetPosition = true;
            }
        }
    }
    
    private void HealthChanged(object sender, EventArgs e)
    {
        // blood runs out!
        if (_healthSystem.GetHealth() == 0)
        {
            // this enemy dead
        }
    }
}
