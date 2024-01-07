using System;
using Cinemachine;
using UnityEngine;

namespace Game_Folders.Scripts
{
    public class CameraSetting : MonoBehaviour
    {
        private CinemachineConfiner2D _cinemachineConfiner2D;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private void Start()
        {
            var player = GameObject.FindWithTag("Player").transform;
            _cinemachineConfiner2D = GetComponent<CinemachineConfiner2D>();
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

            // assign object to
            _cinemachineVirtualCamera.m_Follow = player;
            _cinemachineConfiner2D.m_BoundingShape2D = GameObject.FindWithTag("Confiner").GetComponent<PolygonCollider2D>();
        }
    }
}