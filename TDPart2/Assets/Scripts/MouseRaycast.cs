using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    public Manager manager;
    public Popup popupWindow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if(clickedObject.tag == "enemy") {
                        clickedObject.GetComponent<MobaEnemy>().hit(1);
                    }

                    //TODO: click on possible tower placement and place a tower
                    if(clickedObject.tag == "placement") {
                        popupWindow.Open(clickedObject);
                    }
                }
            }
        }
    }
}
