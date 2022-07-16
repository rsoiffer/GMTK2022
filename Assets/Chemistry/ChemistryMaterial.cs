/*
Materials are affected by elements
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryMaterial : MonoBehaviour
{
    public bool Heatable;
    
    // Events for element interactions
    public delegate void ElementReaction(ChemistryElement element);
    public event ElementReaction ElementAdded;
    public event ElementReaction ElementRemoved;
    
    public void OnElementAdd(ChemistryElement element)
    {
        print("Element added");
        if (ElementAdded != null)
        {
            ElementAdded(element);
        }
    }
    
    public void OnElementRemove(ChemistryElement element)
    {
        if (ElementRemoved != null)
        {
            ElementRemoved(element);
        }
    }
}
