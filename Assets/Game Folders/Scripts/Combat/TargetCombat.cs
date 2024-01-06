using UnityEngine;

namespace Combat
{
    public class TargetCombat : MonoBehaviour
    {
        private HealthSystem _healthSystem;
        public HealthSystem Health
        {
            get
            {
                return _healthSystem;
            }
            set
            {
                _healthSystem = value;
            }
        }
    }
}