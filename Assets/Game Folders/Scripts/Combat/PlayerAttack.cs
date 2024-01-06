using UnityEngine;

namespace Combat
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private int basicAttack = 3;
        
        private GameObject _attackArea = default;
        private bool _attacking = false;
        private float _timeToAttack = 0.25f;
        private float _timer = 0f;
        
        void Start()
        {
            // setup damage on attack area basic attack ~
            _attackArea = transform.Find("attackArea").gameObject;
            _attackArea.GetComponent<AttackArea>().SetDamageArea(basicAttack);
        }
        
        void Update()
        {
            // basic attack
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            if(_attacking)
            {
                _timer += Time.deltaTime;

                if(_timer >= _timeToAttack)
                {
                    _timer = 0;
                    _attacking = false;
                    _attackArea.SetActive(_attacking);
                }

            }
        }

        private void Attack()
        {
            _attacking = true;
            _attackArea.SetActive(_attacking);
        }
    }
}