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
        catDelay = 0.99f;
        catSpeed = 1.5f;
        cupboardSpeed = 0.2f;
        stirSpeed = 15f;
        tempSpeed = 20f;
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

    private void Update()
    {
        //print(1.0/Time.deltaTime);
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
