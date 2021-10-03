using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stir : MonoBehaviour
{
    private WitchesBrew witchesBrew;
    private InputAction stir;
    private Transform pivot;
    private float angle;

    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Stir.performed += StirPot;
        
        pivot = transform.parent;
        angle = 0;
    }

    private void StirPot(InputAction.CallbackContext obj)
    {
        angle += 10;
    }

    private void OnEnable()
    {
        stir = witchesBrew.Player.Stir;
        stir.Enable();
        
    }

    private void OnDisable()
    {
        stir.Disable();
    }

    private void Update()
    {
        angle -= 0.05f;
        transform.RotateAround(pivot.position,Vector3.back, angle* Time.deltaTime);
    }
}
