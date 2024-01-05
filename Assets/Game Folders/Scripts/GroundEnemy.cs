using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    public float moveSpeed;
    private bool movingTowardTargetPosition;
    public SpriteRenderer srEnemy;

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
                srEnemy.flipX = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                movingTowardTargetPosition = true;
                srEnemy.flipX = false;
            }
        }
    }

    // OnCollisionEnter2D = Get called on the frame that another collider has hit another collider
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        other.gameObject.GetComponent<Player>().GameOver();
    //    }
    //}
}
