using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stir : MonoBehaviour
{
    private WitchesBrew witchesBrew;
    private InputAction stir;
    //private Transform pivot;
    private float angle;
    private float deltaAngle;
    private bool changeStir;

    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Stir.performed += StirPot;
        //pivot = transform.parent;
        angle = 0;
        changeStir = false;
    }

    private void Start()
    {
        deltaAngle = GameManager.Instance.GETStirSpeed();
    }

    private void StirPot(InputAction.CallbackContext obj)
    {
        changeStir = !changeStir;
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
        if (changeStir)
        {
            angle -= deltaAngle;
        }
        else
        {
            angle += deltaAngle;
        }

        transform.eulerAngles = new Vector3(0,0,angle);
        if (angle > 33 || angle < -33)
        {
            GameManager.Instance.LoseLife();
            angle = 0;
        }
    }
}
