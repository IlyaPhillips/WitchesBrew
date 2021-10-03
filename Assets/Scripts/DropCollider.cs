using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropCollider : MonoBehaviour
{
    [SerializeField] private GameObject droppedObject;

    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = droppedObject.GetComponent<IngredientArc>().ArcVelocity(transform.position, 1.5f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        droppedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        droppedObject.GetComponent<Rigidbody2D>().velocity = velocity;
        droppedObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-30f,30f);
    }
}
