using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : ChemistryElement
{
    public override void InteractWith(ChemistryMaterial material)
    {
        if (material.Heatable)
        {
            material.gameObject.AddComponent<Fire>();
        }
    }
    
    public override void InteractWith(ChemistryElement element)
    {
        
    }
}
