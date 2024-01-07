using System;
using UnityEngine;

namespace Game_Folders.Scripts
{
    public class SetupState : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.ChangeState(Gamestate.Level);
        }
    }
}