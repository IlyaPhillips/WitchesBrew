using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AdjustHeat : MonoBehaviour
{
    [SerializeField]private float temperature;
    private float adjust;
    private float tempDelta;
    private WitchesBrew witchesBrew;
    private InputAction adjustHeat;
    private Transform pivot;

    // Update is called once per frame
    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.AdjustFire.performed += Move;
        temperature = 0.5f;
        pivot = transform.parent;
        adjust = 0;
        tempDelta = 5f;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        adjust = -ctx.ReadValue<float>() * 10f;
        print(adjust);
    }

    private void OnEnable()
    {
        adjustHeat = witchesBrew.Player.AdjustFire;
        adjustHeat.Enable();
        
    }

    private void OnDisable()
    {
        adjustHeat.Disable();
    }

    private void Update()
    {
        temperature = transform.eulerAngles.z;
        tempDelta *= Mathf.Sign(temperature);
        transform.RotateAround(pivot.position,Vector3.forward, (adjust+tempDelta)*Time.deltaTime);
    }
}
