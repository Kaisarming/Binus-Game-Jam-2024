using UnityEngine;

namespace Combat
{
    public class AttackArea : MonoBehaviour
    {
       private int damage = 1;

       internal void SetDamageArea(int damage)
       {
           this.damage = damage;
       }
       
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<TargetCombat>() != null)
            {
                var targetCombat = collider.GetComponent<TargetCombat>();
                var targetHealth = targetCombat.Health;
                
                targetHealth.Damage(damage);
            }
        }
    }
}