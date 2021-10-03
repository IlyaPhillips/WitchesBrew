using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private float speed;

    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        speed = GameManager.Instance.GETCatSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*speed);
        if (Vector3.Distance(start, transform.position) > 24) Destroy(gameObject);
    }
}
