using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientArc : MonoBehaviour
{
    private Transform cauldron;

    private Rigidbody2D rb;

    [SerializeField] private bool shelf;
    // Start is called before the first frame update
    void Start()
    {
        
        cauldron = GameManager.Instance.GETCauldron();
        rb = GetComponent<Rigidbody2D>();
        if (shelf) return;
        rb.velocity = ArcVelocity(transform.position, 1.5f);
        rb.angularVelocity = Random.Range(50f, 80f);
    }

    // Update is called once per frame
    public Vector2 ArcVelocity(Vector2 start, float time)
    {
        Vector2 velocity;
        Vector2 target = cauldron.position;
        float targetGrav = target.y - Physics2D.gravity.y/(2*time);
        var gravOffset = -(time-1) * Physics2D.gravity.y;
        velocity.x = (target.x - start.x)/time;
        velocity.y = (targetGrav - start.y)/time + gravOffset;
        return velocity;
    }

    public bool GETShelf()
    {
        return shelf;
    }
}
