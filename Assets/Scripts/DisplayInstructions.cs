using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInstructions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<String> instructions;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Image background;
    
    [SerializeField, Header("Config")] 
    private float displayTime = 3.5f;
    [SerializeField] private float fadeInTime = 1f;
    [SerializeField] private float backgroundMaxAlpha = 0.8f;

    
    private int currentInstructionIndex = 0;

    private void Awake()
    {
        displayText.alpha = 0f;
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
    }

    private void OnEnable()
    {
        gameManager.OnGameStateChange += DisplayEachInstruction;
    }

    private void OnDisable()
    {
        gameManager.OnGameStateChange -= DisplayEachInstruction;
    }

    private void DisplayEachInstruction(GameState state)
    {
        StartCoroutine(FadeSequence());
        displayText.text = instructions[currentInstructionIndex];
        currentInstructionIndex++; 
    
    }
        
    private IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeText(0f, 1f, 0f, backgroundMaxAlpha));
        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(FadeText(1f, 0f, backgroundMaxAlpha, 0f));
    }

    
    private IEnumerator FadeText(float startTextAlpha, float endTextAlpha, 
        float startBackgroundAlpha, float endBackgroundAlpha)
    {
        displayText.gameObject.SetActive(true);
        float elapsedTime = 0f;
        
        Color textColor = displayText.color;
        Color backgroundColor = background.color;
        textColor.a = startTextAlpha;
        backgroundColor.a = startBackgroundAlpha;
        
        displayText.color = textColor;
        background.color = backgroundColor;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeInTime;
            
            textColor.a = Mathf.Lerp(startTextAlpha, endTextAlpha, progress);
            backgroundColor.a = Mathf.Lerp(startBackgroundAlpha, endBackgroundAlpha, progress);
            
            displayText.color = textColor;
            background.color = backgroundColor;
            yield return null;
        }
        
        textColor.a = endTextAlpha;
        backgroundColor.a = endBackgroundAlpha;
        displayText.color = textColor;
        background.color = backgroundColor;

       
    }

    

    // IEnumerator WaitToHide()
    // {
    //     yield return new WaitForSeconds(displayTime);
    //     StartCoroutine(FadeOutText());
    // }
}