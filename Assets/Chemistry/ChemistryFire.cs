using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryFire : ChemistryElement
{
    public override void InteractWith(ChemistryMaterial material)
    {
        if (material.Flammable)
        {
            print("Burn!");
        }
    }
    
    public override void InteractWith(ChemistryElement element)
    {
        
    }
}
