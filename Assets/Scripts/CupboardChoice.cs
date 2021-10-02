
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupboardChoice : MonoBehaviour
{
    [SerializeField] private List<GameObject> cupboards;
    private int index;
    private WitchesBrew witchesBrew;
    private InputAction witchMovement;

    // Update is called once per frame
    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.WitchMove.started += Move;
        index = 0;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        if (index + (int)ctx.ReadValue<float>() >= 0 && index + (int)ctx.ReadValue<float>() < cupboards.Count)
        {
            index += (int)ctx.ReadValue<float>();
            var pos = transform;
            var moveTo = new Vector2(cupboards[index].transform.position.x,pos.position.y);
            pos.position = moveTo;
        }
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
    
}
