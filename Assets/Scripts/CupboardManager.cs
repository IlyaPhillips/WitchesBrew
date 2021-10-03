using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CupboardManager : MonoBehaviour
{
    [SerializeField] private Transform witch;
    [SerializeField] private bool activatingCupboard;

    private List<Transform> cupboards;

    private int activeCupboard;
    private int cupboardTimer;
    private Transform icon;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        cupboards = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cupboards.Add(transform.GetChild(i));
        }
        activeCupboard = 0;
        cupboardTimer = 10;
        speed = GameManager.Instance.GETCupboardSpeed();
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
        StartCoroutine(CupboardActive());
    }

    void CheckWitch()
    {
        if (Math.Abs(witch.position.x - cupboards[activeCupboard].position.x) < 0.2f)
        {
            var cupboardIngredient = cupboards[activeCupboard].GetChild(0).gameObject;
            cupboards[activeCupboard].GetComponent<SpawnIngredient>().OpenCupboard();
        }
        else
        {
            GameManager.Instance.LoseLife();
        }
        icon.GetComponent<SpriteRenderer>().color = Color.white;
        activatingCupboard = true;
    }

    IEnumerator CupboardActive()
    {
        var color = Color.yellow;
         
        for (int i = 0; i < cupboardTimer; i++)
        {
            var orange = new Color(1.0f,0.549f,0);
            color = Color.Lerp(color, orange, 0.05f*i);
            icon.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(speed);
        }
        icon.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(speed);
        
        CheckWitch();
    }
}
