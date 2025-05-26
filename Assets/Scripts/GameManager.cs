using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum GameState 
{
    Start,
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
    public event GameStateChangeHandler OnGameStateChange;
    
    private HashSet<GameState> triggeredStates = new HashSet<GameState>();
    private GameState currentState;
    
    public static GameManager Instance;
    [Header("References")]
    [SerializeField] private Transform catSpawner;
    [SerializeField] private Transform cupboards;
    [SerializeField] private Transform temperature;
    [SerializeField] private Transform stirring;
    [SerializeField] private Transform witch;
    
    [field: SerializeField] public  GameState State { get; private set; }
    private WitchesBrew witchesBrew;
    private InputAction pause;
    [field: SerializeField, HideInInspector] public bool Paused { get; set; }
    private bool nextStage;
    private float timer;
    private GameState prevState;

    [Header("Config"),SerializeField] private float startDelay = 3f ;
    [field: SerializeField ] public Transform Cauldron { get; private set; }
    [field: SerializeField, Tooltip("Low Easier"), Range(0,2)] public float CatDelay { get; private set; } = 0.99f; 
    [field: SerializeField, Tooltip("Low Easier"), Range(0,2)] public float CatSpeed { get; private set; } = 1.5f; 
    [field: SerializeField, Tooltip("High Easier"), Range(0,1)] public float CupboardSpeed { get; private set; } = 0.2f; 
    [field: SerializeField, Tooltip("Low Easier"), Range(0,25)] public float StirSpeed { get; private set; } = 15f;
    [field: SerializeField, Tooltip("Low Easier"),Range(0,40)] public float TempSpeed { get; private set; } = 20f; 
    [field: SerializeField, Range(0,10)] public int Lives { get; private set; } = 5;

    private bool _readyToStart; public event Action OnLoseLife;
    
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        _readyToStart = true;
    }

    private void Awake()
    {
        Instance = this;
        HandleStateChange(GameState.Start);
        witchesBrew = InputInstance.Instance;
        Paused = false;
        nextStage = false;
        timer = 0f;
        DisableAllComponents();
        prevState = GameState.Start;
    }
    
    private void DisableAllComponents()
    {
        stirring.GetComponent<Stir>().enabled = false;
        cupboards.GetComponent<CupboardManager>().enabled = false;
        temperature.GetComponent<AdjustHeat>().enabled = false;
        catSpawner.GetComponent<CatSpawner>().enabled = false;
        witch.GetComponent<WitchCupboardChoice>().enabled = false;
    }


    public void LoseLife()
    {
        Lives--;
        OnLoseLife?.Invoke();
    }

    public void SETGameState(GameState gameState)
    {
        if (State != gameState)
        {
            State = gameState;
            if (!triggeredStates.Contains(gameState))
            {
                if (gameState == GameState.Menu || gameState == GameState.Pause) return;
                triggeredStates.Add(gameState);
                OnGameStateChange?.Invoke(gameState);
            }
        }
    }
    
    private void HandleStateChange(GameState newState)
    {
        if (State != newState)
        {
            SETGameState(newState);
        }
    }

    private void Update()
    {
        if (Paused && State != GameState.Pause)
        {
            prevState = State;
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


        switch (State)
        {
            case GameState.Start:
                break;
            case GameState.Menu:
                break;
            case GameState.Pause:
                if (!Paused)
                {
                    HandleStateChange(prevState); 
                }
                break;
            case GameState.Stage1:
                Time.timeScale = 1;
                witch.GetComponent<WitchCupboardChoice>().enabled = true;
                StartCoroutine(StartDelay());
                if (!_readyToStart) return;
                cupboards.GetComponent<CupboardManager>().enabled = true;
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
                Paused = false;
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