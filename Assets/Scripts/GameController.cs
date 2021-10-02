using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private WitchesBrew witchesBrew;
    private InputAction witchMovement;
    private InputAction stir;
    private InputAction adjustHeat;
    private InputAction deflect;

    private void Awake()
    {
        witchesBrew = new WitchesBrew();
    }

    private void OnEnable()
    {
        witchMovement = witchesBrew.Player.WitchMove;
        witchMovement.Enable();
        
    }

    private void OnDisable()
    {
        witchMovement.Disable();
    }

    private void Update()
    {
        witchMovement.ReadValue<float>();
    }
}
