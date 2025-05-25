using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeCounter : MonoBehaviour
{
    private int lives;

    private bool _finished;
    // Start is called before the first frame update
    void Start()
    {
        lives = GameManager.Instance.Lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (_finished) return;
        if (GameManager.Instance.Lives < lives)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            lives = GameManager.Instance.Lives;
        }
        if (GameManager.Instance.Lives == 0)
        {
            _finished = true;
        }
    }
}
