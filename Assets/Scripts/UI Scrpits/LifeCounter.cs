using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeCounter : MonoBehaviour
{
    private int lives;
    // Start is called before the first frame update
    void Start()
    {
        lives = GameManager.Instance.GETLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GETLives() < lives)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            lives = GameManager.Instance.GETLives();
        }
    }
}
