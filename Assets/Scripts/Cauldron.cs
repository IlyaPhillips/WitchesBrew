using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.GetComponent<IngredientArc>().GETShelf()) {GameManager.Instance.LoseLife(); }

        Destroy(other.gameObject);
    }
}
