
using UnityEngine;


public class PersistantMusic : MonoBehaviour
{
    public static PersistantMusic Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
}
