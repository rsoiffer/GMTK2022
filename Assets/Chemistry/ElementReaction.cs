/*
Base class for components that encode reactions to elements
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementReaction: MonoBehaviour
{
    void Start()
    {
        ChemistryMaterial material = GetComponent<ChemistryMaterial>();
        material.ElementAdded += ElementAddedHandler;
        material.ElementRemoved += ElementRemovedHandler;
    }
    
    protected virtual void ElementAddedHandler(ChemistryElement element) {}
    protected virtual void ElementRemovedHandler(ChemistryElement element) {}
}