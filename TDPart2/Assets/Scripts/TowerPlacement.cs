using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    private Color startcolor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Functions to highlight placement
    void OnMouseEnter()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = new Color32(0, 103, 255, 134);
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startcolor;
    }
}
