using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MagicMouse : MonoBehaviour
{
    private WitchesBrew witchesBrew;
    private InputAction deflect;
    private InputAction clicked;
    private Camera cam;
    private Vector2 mousePos;
    private SpriteRenderer sr;
    private Color colorOff;
    private Color colorOn;
    private bool mouse;

    private Collider2D col;
    // Start is called before the first frame update
    private void Awake()
    {
        witchesBrew = new WitchesBrew();
        witchesBrew.Player.Deflect.performed += MoveMouse;
        witchesBrew.Player.DeflectClick.performed += ClickMouse;
        cam = Camera.main;
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        colorOff = sr.color;
        colorOn = new Color(colorOff.r, colorOff.g, colorOff.b, 225);
        mouse = false;
    }

    private void ClickMouse(InputAction.CallbackContext obj)
    {
        
        if(!mouse) StartCoroutine(BlinkCollider());
        
    }

    private void MoveMouse(InputAction.CallbackContext obj)
    {
        mousePos = obj.ReadValue<Vector2>();
        
        transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,10));
    }

    IEnumerator BlinkCollider()
    {
        col.enabled = true;
        mouse = true;
        sr.color = colorOn;
        yield return new WaitForSeconds(0.35f);
        sr.color = colorOff;
        mouse = false;
        col.enabled = false;
    }

    private void OnEnable()
    {
        deflect = witchesBrew.Player.Deflect;
        clicked = witchesBrew.Player.DeflectClick;
        deflect.Enable();
        clicked.Enable();
    }

    private void OnDisable()
    {
        deflect.Disable();
        clicked.Disable();
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
