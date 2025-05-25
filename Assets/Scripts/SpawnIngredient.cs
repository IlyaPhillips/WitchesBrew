using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] private Sprite closed;
    [SerializeField] private Sprite open;
    [SerializeField] private GameObject ingredient;
    private SpriteRenderer sr;
    private float speed;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = 0.2f;
    }

    public void OpenCupboard()
    {
        sr.sprite = open;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(ingredient, transform);
        yield return new WaitForSeconds(speed);
        sr.sprite = closed;
    }
    
}
