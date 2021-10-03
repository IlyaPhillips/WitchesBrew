using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MagicMouse : MonoBehaviour
{
    private WitchesBrew witchesBrew;
    private InputAction deflect;
    private Camera cam;
    private Vector2 mousePos;
    // Start is called before the first frame update
    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Deflect.performed += MoveMouse;
        cam = Camera.main;
    }

    private void MoveMouse(InputAction.CallbackContext obj)
    {
        mousePos = obj.ReadValue<Vector2>();
        
        transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,10));
    }

    private void OnEnable()
    {
        deflect = witchesBrew.Player.Deflect; 
        deflect.Enable();
    }

    private void OnDisable()
    {
        deflect.Disable();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DroppedIngredient>())
        {
            other.GetComponent<DroppedIngredient>().ReturnToStart();
        }
    }
}
