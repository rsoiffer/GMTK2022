using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReplacementShader : MonoBehaviour
{
    public Camera cam;
    public Shader replacementShader;

    // Start is called before the first frame update
    void Start()
    {
        cam.SetReplacementShader(replacementShader, "Transparent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
