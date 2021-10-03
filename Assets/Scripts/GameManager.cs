using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform cauldron;
    private float catDelay; //high easier
    private float catSpeed; //low easier
    private float cupboardSpeed; //high easier
    private float stirSpeed; //low easier
    private float tempSpeed; //low easier

    private void Awake()
    {
        Instance = this;
        catDelay = 10;
        catSpeed = 0.1f;
        cupboardSpeed = 0.2f;
        stirSpeed = 0.1f;
        tempSpeed = 5f;
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
}

public enum GameState {
    Menu,
    Pause,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage6,
    Stage7,
    Stage8,
    Win,
    Lose

}
