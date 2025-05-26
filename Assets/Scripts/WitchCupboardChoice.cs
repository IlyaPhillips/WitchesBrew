
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WitchCupboardChoice : MonoBehaviour
{
    [SerializeField] private Transform cupboardManager;
    private List<Transform> cupboards;
    private int index;
    private WitchesBrew witchesBrew;
    
    private InputAction witchMovement;


    // Update is called once per frame
    private void Awake()
    {
        witchesBrew = InputInstance.Instance;
        witchesBrew.Player.WitchMove.started += Move;
        cupboards = new List<Transform>();
        for (int i = 0; i < cupboardManager.childCount; i++)
        {
            cupboards.Add(cupboardManager.GetChild(i));
        }

        index = 0;
    }


    private void Move(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.Paused)
        {
            if (index + (int)ctx.ReadValue<float>() >= 0 && index + (int)ctx.ReadValue<float>() < cupboards.Count)
            {
                index += (int)ctx.ReadValue<float>();
                var pos = transform;
                var moveTo = new Vector2(cupboards[index].position.x, pos.position.y);
                pos.position = moveTo;
            }
        }
    }

    private void OnEnable()
    {
        witchMovement = witchesBrew.Player.WitchMove;
        witchMovement.Enable();
    }

    private void OnDisable()
    {
        if (witchMovement != null)
        {
            witchMovement.Disable();
        }
    }
    
    private void OnDestroy()
    {
        if (witchesBrew != null)
        {
            witchesBrew.Player.WitchMove.started -= Move;
        }

    }

    
}
