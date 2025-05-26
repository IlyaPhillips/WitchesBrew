
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Load"),SerializeField]private string sceneName;
    
    public void NextLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
