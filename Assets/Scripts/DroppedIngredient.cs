using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedIngredient : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void ReturnToStart()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = startPos;
        transform.rotation = startRot;
        //StartCoroutine(Magic());
    }

    IEnumerator Magic()
    {
        var toLerp = transform;
        var pos = toLerp.position;
        var rot = toLerp.rotation;
        for (int i = 0; i < 10; i++)
        {
            var lerp = i/10f;
            pos = Vector3.Lerp(pos, startPos, lerp);
            rot = Quaternion.Lerp(rot,startRot,lerp);
            transform.position = pos;
            transform.rotation = rot;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
