using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameManager gameManager;
    private InputAction pause;
    private WitchesBrew witchesBrew;
    
    
    private void Awake()
    {
        witchesBrew = InputInstance.Instance;
        if (witchesBrew == null)
        {
            witchesBrew = new WitchesBrew();
        }
        witchesBrew.Player.Pause.performed += OnPause;

    }

    private void OnEnable()
    {
        pause = witchesBrew.Player.Pause;
        pause.Enable();
    }

    private void OnDisable()
    {
        if (pause != null) 
        {
            pause.Disable();
        }
        if (witchesBrew != null)
        {
            witchesBrew.Player.Pause.performed -= OnPause;
        }

    }
    
    private void OnPause(InputAction.CallbackContext context)
    {
      
        if (GameManager.Instance.State != GameState.Lose)
        {
            if (GameManager.Instance.State == GameState.Pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        gameManager.Paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameManager.Paused = false;

    }
}