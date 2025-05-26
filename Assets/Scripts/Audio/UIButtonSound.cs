using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    
    [Header("UI Buttons"), SerializeField] private AudioClip uiButton;
    [Header("UI Buttons"), SerializeField] private AudioSource source;
    
    private void OnEnable()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(UiClips);
        }
    }

    private void OnDisable()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveListener(UiClips);
        }
    }
    
    private void UiClips()
    {
        source.PlayOneShot(uiButton);
    }
    

}
