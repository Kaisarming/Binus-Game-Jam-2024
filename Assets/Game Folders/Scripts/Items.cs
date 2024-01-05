using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    protected virtual void CollectItem()
    {
        Destroy(gameObject);
    }
}
