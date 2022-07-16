/*
Materials are affected by elements
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryMaterial : MonoBehaviour
{
    // Throwing around property ideas ¯\_(ツ)_/¯
    public bool Flammable;
    
    // Events for element interactions
    public delegate void ElementReaction(ChemistryElement element);
    public event ElementReaction FireInteraction;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        ChemistryElement element = other.gameObject.GetComponent<ChemistryElement>();
        if (element)
        {
            //if (element is Fire)
            {
                
            }
        }
    }
}
