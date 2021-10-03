using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientArc : MonoBehaviour
{
    private Transform cauldron;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        cauldron = GameManager.Instance.getCauldron();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = ArcVelocity(transform.position, cauldron.position, 1.5f);
        rb.angularVelocity = Random.Range(50f, 80f);

    }

    // Update is called once per frame
    private Vector2 ArcVelocity(Vector2 start,Vector2 target, float time)
    {
        Vector2 velocity;
        float targetGrav = target.y - Physics2D.gravity.y/(2*time);
        var gravOffset = -(time-1) * Physics2D.gravity.y;
        velocity.x = (target.x - start.x)/time;
        velocity.y = (targetGrav - start.y)/time + gravOffset;
        return velocity;
    }
}
