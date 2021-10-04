using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FPS : MonoBehaviour
{
    private TextMeshPro text; 
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        string fps = "FPS: " + (1.0f / Time.deltaTime).ToString();
        text.text = fps;
    }
}
