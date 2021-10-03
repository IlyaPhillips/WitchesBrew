using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] private Sprite closed;
    [SerializeField] private Sprite open;
    [SerializeField] private GameObject ingredient;
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OpenCupboard()
    {
        sr.sprite = open;
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        print("cupboard spawn");
        yield return new WaitForSeconds(0.5f);
        sr.sprite = closed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
