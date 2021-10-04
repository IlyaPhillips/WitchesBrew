using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientArc))]
public class DroppedIngredient : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody2D rb;

    private IngredientArc arc;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        arc = GetComponent<IngredientArc>();
    }

    // Update is called once per frame
    public void ReturnToStart()
    {
        rb.isKinematic = true;
        arc.enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = startPos;
        transform.rotation = startRot;
    }

    
}
