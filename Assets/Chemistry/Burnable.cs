/*
Component for stuff that gets damaged when exposed to fire
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable: ElementReaction
{
    protected override void ElementAddedHandler(ChemistryElement element)
    {
        base.ElementAddedHandler(element);
        
        if (element is Fire)
        {
            print("ouch fire");
        }
        else
        {
            print("yep that's an element");
        }
    }
}