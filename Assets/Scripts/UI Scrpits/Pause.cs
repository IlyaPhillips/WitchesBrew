using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GETGameState() == GameState.Pause ||
            GameManager.Instance.GETGameState() == GameState.Lose)
        {
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
        }
    }
}
