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

    // Update is called once per frame
    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.AdjustFire.performed += Move;
        witchesBrew.Player.AdjustFire.canceled += Move;
        temperature = 0.5f;
        adjust = 0;
    }

    private void Start()
    {
        tempDelta = GameManager.Instance.TempSpeed;

    }

    private void Move(InputAction.CallbackContext ctx)
    {
        adjust = -ctx.ReadValue<float>() * tempDelta *2;
        tempDelta = GameManager.Instance.TempSpeed;
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
        var sign= Mathf.Sign(temperature);
        temperature += (((sign*tempDelta) + adjust)*Time.deltaTime);
        var change = new Vector3(0, 0, temperature);
        transform.eulerAngles = change;
        if (temperature > 85 || temperature < -85)
        {
            temperature -= temperature / 3f;
            GameManager.Instance.LoseLife();
        }
    }
}
