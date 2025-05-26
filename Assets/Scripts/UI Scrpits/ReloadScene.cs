using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    [SerializeField] private float delay = 0.2f;
    
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(Delay());
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        LoadLevel();
    }
    
    private void LoadLevel()
    {
        print("scene load");
        SceneManager.LoadScene(sceneName);
    }
    
}
