using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInstance : MonoBehaviour
{
    private static WitchesBrew instance;
    
    public static WitchesBrew Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WitchesBrew();
            }
            return instance;
        }
    }

}
