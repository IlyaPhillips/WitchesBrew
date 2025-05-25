using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameManager gameManager;

    private static readonly int StartStirring = Animator.StringToHash("StartStirring");

    private void OnEnable()
    {
        gameManager.OnGameStateChange += TriggerAnims;
    }
    
    private void OnDisable()
    {
        gameManager.OnGameStateChange -= TriggerAnims;
    }

    private void TriggerAnims(GameState state)
    {
        if (state == GameState.Stage2)
        {
            animator.SetTrigger(StartStirring);
        }
    }
}
