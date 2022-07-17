using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporable : MonoBehaviour
{
    public float evaporationTime = 1;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Air Element") || col.gameObject.CompareTag("Fire Element"))
        {
            evaporationTime -= Time.deltaTime;            
            if (evaporationTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
