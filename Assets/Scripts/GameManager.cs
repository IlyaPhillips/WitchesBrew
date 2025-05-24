using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum GameState 
{
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
public class GameManager : MonoBehaviour
{
    public delegate void GameStateChangeHandler(GameState state);
    public static event GameStateChangeHandler OnGameStateChange;
    private HashSet<GameState> triggeredStates = new HashSet<GameState>();
    private GameState currentState;
    
    public static GameManager Instance;
    [Header("References")]
    [SerializeField] private Transform cauldron;
    [SerializeField] private Transform catSpawner;
    [SerializeField] private Transform cupboards;
    [SerializeField] private Transform temperature;
    [SerializeField] private Transform stirring;
    [SerializeField] private Transform witch;
    [SerializeField]private GameState state;
    private WitchesBrew witchesBrew;
    private InputAction pause;
    private bool paused;
    private bool nextStage;
    private float timer;
    private GameState prevState;
    
    [field: SerializeField, Header("Gameplay Parameters"), Tooltip("Low Easier"), Range(0,2)] public float CatDelay { get; private set; } = 0.99f; 
    [field: SerializeField, Tooltip("Low Easier"), Range(0,2)] public float CatSpeed { get; private set; } = 1.5f; 
    [field: SerializeField, Tooltip("High Easier"), Range(0,1)] public float CupboardSpeed { get; private set; } = 0.2f; 
    [field: SerializeField, Tooltip("Low Easier"), Range(0,25)] public float StirSpeed { get; private set; } = 15f;
    [field: SerializeField, Tooltip("Low Easier"),Range(0,40)] public float TempSpeed { get; private set; } = 20f; 
    [field: SerializeField, Range(0,10)] public int Lives { get; private set; } = 5;

    

    private void Awake()
    {
        Instance = this;
        HandleStateChange(GameState.Stage1);
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

    public void LoseLife()
    {
        Lives--;
    }

    public GameState GETGameState()
    {
        return state;
    }

    public void SETGameState(GameState gameState)
    {
        if (state != gameState)
        {
            state = gameState;
            if (!triggeredStates.Contains(gameState))
            {
                triggeredStates.Add(gameState);
                OnGameStateChange?.Invoke(gameState);
            }
        }
    }
    
    private void HandleStateChange(GameState newState)
    {
        if (state != newState)
        {
            SETGameState(newState);
        }
    }

    private void Update()
    {
        if (paused)
        {
            prevState = state;
            HandleStateChange(GameState.Pause);
        }

        if (Lives < 0)
        {
            HandleStateChange(GameState.Lose);
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
                    HandleStateChange(GameState.Stage2);
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
                    HandleStateChange(GameState.Stage3);
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
                    HandleStateChange(GameState.Stage4);
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
                    HandleStateChange(GameState.Stage5);
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
                    HandleStateChange(GameState.Win);
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