using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private GameState state;
    private WitchesBrew witchesBrew;
    private InputAction pause;
    private bool paused;
    private bool nextStage;
    private float timer;

    private void Awake()
    {
        Instance = this;
        catDelay = 0.99f;
        catSpeed = 1.5f;
        cupboardSpeed = 0.2f;
        stirSpeed = 15f;
        tempSpeed = 20f;
        lives = 3;
        state = GameState.Menu;
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Pause.performed += Pause;
        paused = false;
        nextStage = false;
        timer = 0f;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        paused = !paused;
    }

    private void OnEnable()
    {
        pause = witchesBrew.Player.Pause;
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
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

    public int GETLives()
    {
        return lives;
    }

    public GameState GETGameState()
    {
        return state;
    }

    private void Update()
    {
        if (paused)
        {
            state = GameState.Pause;
        }

        if (lives < 0)
        {
            state = GameState.Lose;
        }

        if (Time.time - timer > 30f)
        {
            nextStage = true;
        }


        switch (state)
        {
            case GameState.Menu:
                // menu
                break;
            case GameState.Pause:
                //pause
                break;
            case GameState.Stage1:
                //stir
                if (nextStage)
                {
                    nextStage = false;
                    state = GameState.Stage2;
                    timer = Time.time;
                }
                break;
            case GameState.Stage2:
                //stir
                //cupboards
                if (nextStage)
                {
                    nextStage = false;
                    state = GameState.Stage3;
                    timer = Time.time;
                }
                break;
            case GameState.Stage3:
                //stir
                //cupboards
                //temperature
                if (nextStage)
                {
                    nextStage = false;
                    state = GameState.Stage4;
                    timer = Time.time;
                }
                break;
            case GameState.Stage4:
                //stir
                //cupboards
                //temperature
                //cats
                if (nextStage)
                {
                    nextStage = false;
                    state = GameState.Stage5;
                    timer = Time.time;
                }
                break;
            case GameState.Stage5:
                //stir
                //cupboards
                //temperature
                //cats
                //increasing difficulty
                if (nextStage)
                {
                    nextStage = false;
                    state = GameState.Win;
                }
                break;
            case GameState.Win:
                //1 minute at stage 5
                break;
            case GameState.Endless:
                //accessed from win
                break;
            case GameState.Lose:
                //lose 3 lives
                //restart from last stage
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
    Win,
    Endless,
    Lose

}
