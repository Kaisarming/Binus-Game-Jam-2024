using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(TargetCombat))]
public class EnemyFlyGFX : MonoBehaviour
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
    
    public AIPath aiPath;

    public AIDestinationSetter aiDestinationSetter;

    private void Awake()
    {
        // health setup
        var newHealthBar = Instantiate(pfHealthBar, pointHealthBar.position, Quaternion.identity, transform);
        var healthBar = newHealthBar.GetComponent<HealthBar>();
        _healthSystem = new HealthSystem(healthMax);
        healthBar.Setup(_healthSystem);
        
        // assign health to target combat component
        GetComponent<TargetCombat>().Health = _healthSystem;
        
        // add listener to health system
        _healthSystem.OnHealthChanged += HealthChanged;
    }

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");

        aiDestinationSetter.target = player.transform;
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
    private void HealthChanged(object sender, EventArgs e)
    {
        // blood runs out!
        if (_healthSystem.GetHealth() == 0)
        {
            // this enemy dead
            Destroy(this.gameObject);
        }
    }
}
