using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CupboardManager : MonoBehaviour
{
    [SerializeField] private Transform witch;
    [SerializeField] private bool activatingCupboard;

    private List<Transform> cupboards;

    private int activeCupboard;

    private Transform icon;
    // Start is called before the first frame update
    void Start()
    {
        cupboards = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cupboards.Add(transform.GetChild(i));
        }
        activeCupboard = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activatingCupboard)
        {
            PickCupboard();
            activatingCupboard = false;
        }
    }

    void PickCupboard()
    {
        activeCupboard = Random.Range(0, cupboards.Count);
        icon = cupboards[activeCupboard].GetChild(0);
        print(icon.name);
        
        
    }

    // IEnumerator CupboardActive()
    // {
    //     var color = Color.Lerp(Color.yellow, Color.red, 0.3f);
    //     icon.GetComponent<SpriteRenderer>().color = color* Time.deltaTime;
    // }
}
