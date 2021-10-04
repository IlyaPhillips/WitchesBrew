using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("References")]
    [SerializeField] private Transform cauldron;
    [SerializeField] private Transform catSpawner;
    [SerializeField] private Transform cupboards;
    [SerializeField] private Transform temperature;
    [SerializeField] private Transform stirring;
    [SerializeField] private Transform witch;
    private float catDelay; //low easier
    private float catSpeed; //low easier
    private float cupboardSpeed; //high easier
    private float stirSpeed; //low easier
    private float tempSpeed; //low easier
    private int lives;
    [SerializeField]private GameState state;
    private WitchesBrew witchesBrew;
    private InputAction pause;
    private bool paused;
    private bool nextStage;
    private float timer;
    private GameState prevState;
    

    private void Awake()
    {
        Instance = this;
        catDelay = 0.99f;
        catSpeed = 1.5f;
        cupboardSpeed = 0.2f;
        stirSpeed = 15f;
        tempSpeed = 20f;
        lives = 5;
        state = GameState.Stage1;
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Pause.performed += Pause;
        paused = false;
        nextStage = false;
        timer = 0f;
        stirring.GetComponent<Stir>().enabled = false;
        cupboards.GetComponent<CupboardManager>().enabled = false;
        temperature.GetComponent<AdjustHeat>().enabled = false;
        catSpawner.GetComponent<CatSpawner>().enabled = false;
        witch.GetComponent<WitchCupboardChoice>().enabled = false;
        prevState = GameState.Pause;
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

    public void SETGameState(GameState gameState)
    {
        state = gameState;
    }

    private void Update()
    {
        if (paused)
        {
            prevState = state;
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
                Time.timeScale = 0;
                witch.GetComponent<WitchCupboardChoice>().enabled = false;
                if (!paused)
                {
                    Time.timeScale = 1;
                    witch.GetComponent<WitchCupboardChoice>().enabled = true;
                    state = prevState;
                }

                break;
            case GameState.Stage1:
                Time.timeScale = 1;
                cupboards.GetComponent<CupboardManager>().enabled = true;
                witch.GetComponent<WitchCupboardChoice>().enabled = true;
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
                
                stirring.GetComponent<Stir>().enabled = true;
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
                temperature.GetComponent<AdjustHeat>().enabled = true;
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
                catSpawner.GetComponent<CatSpawner>().enabled = true;
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
                Time.timeScale = 1.5f;
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
                Time.timeScale = 0;
                paused = false;
                nextStage = false;
                timer = 0f;
                stirring.GetComponent<Stir>().enabled = false;
                cupboards.GetComponent<CupboardManager>().enabled = false;
                temperature.GetComponent<AdjustHeat>().enabled = false;
                catSpawner.GetComponent<CatSpawner>().enabled = false;
                witch.GetComponent<WitchCupboardChoice>().enabled = false;
                prevState = GameState.Pause;
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
