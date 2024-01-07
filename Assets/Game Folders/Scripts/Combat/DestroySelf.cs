using UnityEngine;

namespace Combat
{
    public class DestroySelf : MonoBehaviour
    {
        void PlaySoundAttack()
        {
            AudioManager.Instance.MainkanSuaraSekali("Attack");
        }
        
        void DestroyItSelf()
        {
            Destroy(gameObject);
        }
    }
}