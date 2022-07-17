using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporable : MonoBehaviour
{
    private float evaporationTimer = 1;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Air Element"))
        {
            evaporationTimer -= Time.deltaTime;            
            if (evaporationTimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
