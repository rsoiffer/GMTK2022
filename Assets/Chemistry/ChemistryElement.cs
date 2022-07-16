/*
Base class for an element in the chemistry system. 
If a GameObject has an element component, then that object has the properties of that element. An object with a water
component is wet, for example.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChemistryElement : MonoBehaviour
{
    // The material of the object that has this element. Used to make it emit events.
    private ChemistryMaterial _material;
    
    public abstract void InteractWith(ChemistryMaterial material);
    public abstract void InteractWith(ChemistryElement element);
    
    void Start()
    {
        _material = GetComponent<ChemistryMaterial>();
        
        if (_material)
        {
            _material.OnElementAdd(this);
        }
    }
    
    void OnDestroy()
    {
        if (_material)
        {
            _material.OnElementRemove(this);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        ChemistryElement element = other.gameObject.GetComponent<ChemistryElement>();
        if (element)
        {
            InteractWith(element);
        }
        
        ChemistryMaterial material = other.gameObject.GetComponent<ChemistryMaterial>();
        if (material)
        {
            InteractWith(material);
        }
    }
}
