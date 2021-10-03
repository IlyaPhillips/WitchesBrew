using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropCollider : MonoBehaviour
{
    [SerializeField] private GameObject droppedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        droppedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        droppedObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-30f,30f);
    }
}
