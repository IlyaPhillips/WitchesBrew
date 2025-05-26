using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    // Start is called before the first frame update

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.SETGameState(GameState.Stage1);
        SceneManager.LoadScene("WitchCauldron");
    }

    // Update is called once per frame
   
}
