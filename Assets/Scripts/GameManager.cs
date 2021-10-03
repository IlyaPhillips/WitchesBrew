using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform cauldron;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    public Transform getCauldron()
    {
        return cauldron;
    }
}

public enum GameState {
    
}
