using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInstance : MonoBehaviour
{
    private static WitchesBrew _instance;

    public static WitchesBrew Instance
    {
        get
        {
            if (_instance == null)
            {
                try
                {
                    _instance = new WitchesBrew();
                    _instance.Enable();
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to initialize input system: {e.Message}");
                }
            }

            return _instance;
        }
    }
}
