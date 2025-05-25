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
    
    [SerializeField, Header("Config")] private float displayTime = 3.5f;
    [SerializeField, Header("Config")] private float fadeInTime = 1f;

    
    private int currentInstructionIndex = 0;
    
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
        StartCoroutine(FadeInText());
        displayText.text = instructions[currentInstructionIndex];
        currentInstructionIndex++;       
    }
    
    private IEnumerator FadeInText()
    {
        Color currentColor = background.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0f, 0.8f, elapsedTime / fadeInTime);
            background.color = currentColor;
            yield return null;
        }
        currentColor.a = 0.8f;
        background.color = currentColor;
        if (currentColor.a >= 0.8f)
        {
            displayText.gameObject.SetActive(true);
            StartCoroutine(WaitToHide());
        }
    }
    
    private IEnumerator FadeOutText()
    {
        Color currentColor = background.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0.8f, 0f, elapsedTime / fadeInTime);
            background.color = currentColor;
            yield return null;
        }
        currentColor.a = 0f;
        background.color = currentColor;
        if (currentColor.a <= 0f)
        {
            displayText.gameObject.SetActive(false);
        }
    }
    

    IEnumerator WaitToHide()
    {
        yield return new WaitForSeconds(displayTime);
        StartCoroutine(FadeOutText());
    }
}
