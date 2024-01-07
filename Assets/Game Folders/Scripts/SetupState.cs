using System;
using UnityEngine;

namespace Game_Folders.Scripts
{
    public class SetupState : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.ChangeState(Gamestate.Level);
            var player = GameObject.FindWithTag("Player").GetComponent<Player>();

            // set player jump 
            if (GameManager.Instance.GetActiveLevelData().levelIndex == 2 ||
                GameManager.Instance.GetActiveLevelData().levelIndex == 3)
            {
                player.jump = 24;
            }
            else
            {
                player.jump = 18;
            }
        }
    }
}