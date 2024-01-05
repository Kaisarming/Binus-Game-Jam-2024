using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    public float moveSpeed;
    private bool movingTowardTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        movingTowardTargetPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTowardTargetPosition == true)
        {
            //MoveTowards formula (Same for Vector2) = Vector3.MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                movingTowardTargetPosition = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                movingTowardTargetPosition = true;
            }
        }
    }
}
