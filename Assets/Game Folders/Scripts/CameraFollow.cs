using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Vector3 pivot;

    private void LateUpdate()
    {
        if(player == null)
        {
            return;
        }

        transform.position = player.position + pivot;
    }
}
