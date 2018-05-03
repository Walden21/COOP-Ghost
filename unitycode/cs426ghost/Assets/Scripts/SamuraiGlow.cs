using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiGlow : MonoBehaviour {

    public GameObject ghost1;
    public GameObject ghost2;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);
    }

    void Update()
    {
        if ((ghost1.transform.position - this.transform.position).sqrMagnitude < 1 || (ghost2.transform.position - this.transform.position).sqrMagnitude < 1)
        {
            rend.material.SetColor("_SpecColor", Color.blue);
        }
        else
        {
            rend.material.SetColor("_SpecColor", Color.red);
        }
    }
}


