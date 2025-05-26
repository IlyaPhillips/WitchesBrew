
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Load"),SerializeField]private string sceneName;
    [SerializeField] private float delay = 0.2f;
    
    public void NextLevel()
    {
        if (Time.timeScale <= 0f) Time.timeScale = 1f;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        LoadLevel();
    }
    
    private void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
