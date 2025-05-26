using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CupboardManager cupboardManager;
    [SerializeField] private GameManager gameManger;
    [SerializeField] private AudioSource generalSource;

    [Header("Potion Lives"), SerializeField] private AudioClip potionLives;
    [Header("Cupboard Item"), SerializeField] private AudioClip cupboardItem;
 

    private void OnEnable()
    {
        gameManger.OnLoseLife += FeedPotionClip;
        cupboardManager.OnCupboardOpen += FeedOpenCLip;
    }

    private void OnDisable()
    {
        gameManger.OnLoseLife -= FeedPotionClip;
        cupboardManager.OnCupboardOpen -= FeedOpenCLip;
    }
    
    
    private void FeedPotionClip()
    {
        PlayClip(potionLives);
    }
    
    private void FeedOpenCLip()
    {
        PlayClip(cupboardItem);
    }
    
    private void PlayClip(AudioClip clip)
    {
        generalSource.PlayOneShot(clip);
    }
}
