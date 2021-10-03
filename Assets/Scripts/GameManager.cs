using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform cauldron;
    private float catDelay; //low easier
    private float catSpeed; //low easier
    private float cupboardSpeed; //high easier
    private float stirSpeed; //low easier
    private float tempSpeed; //low easier
    private int lives;

    private void Awake()
    {
        Instance = this;
        catDelay = 0.9995f;
        catSpeed = 0.01f;
        cupboardSpeed = 0.2f;
        stirSpeed = 0.1f;
        tempSpeed = 0.1f;
        lives = 3;
    }
    
    public Transform GETCauldron()
    {
        return cauldron;
    }

    public float GETCatDelay()
    {
        return catDelay;
    }
    
    public float GETCatSpeed()
    {
        return catSpeed;
    }
    
    public float GETCupboardSpeed()
    {
        return cupboardSpeed;
    }
    
    public float GETStirSpeed()
    {
        return stirSpeed;
    }
    public float GETTempSpeed()
    {
        return tempSpeed;
    }

    public void LoseLife()
    {
        lives--;
    }
}

public enum GameState {
    Menu,
    Pause,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Stage6,
    Stage7,
    Win,
    Lose

}
