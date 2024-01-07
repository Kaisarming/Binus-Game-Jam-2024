using System;
using UnityEngine;

namespace Combat
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int basicAttack = 1;
        [SerializeField] private float delayAttack = 0.25f;
        
        // [SerializeField] private GameObject pfSlashEffect;
        // [SerializeField] private Transform pointSlashEffect;

        private GameObject _attackArea = default;
        
        private bool _attacking = false;
        
        private float _timer = 0f;

        private Player _player;
        
        void Start()
        {
            // setup damage on attack area basic attack ~
            _attackArea = transform.Find("attackArea").gameObject;
            _attackArea.GetComponent<AttackArea>().SetDamageArea(basicAttack);
            
            // find a player
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        void Update()
        {
            if (_attacking)
            {
                _timer += Time.deltaTime;

                if (_timer >= delayAttack)
                {
                    _timer = 0;
                    _attacking = false;
                    _attackArea.SetActive(_attacking);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _attacking = true;
                _attackArea.SetActive(true);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _attacking = true;
                _attackArea.SetActive(true);
            }
        }

        private void Attack()
        {
            _attacking = true;
            _attackArea.SetActive(_attacking);

            // create effect slash
            // var slash = Instantiate(pfSlashEffect, pointSlashEffect.position, Quaternion.identity, transform);
            // slash.transform.localScale = new Vector3(
            //     Mathf.Abs(slash.transform.localScale.x) * _attackArea.transform.localScale.x,
            //     1.5f, 1.5f);
            
        }
    }
}