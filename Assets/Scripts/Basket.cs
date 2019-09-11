using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Basket : MonoBehaviour
{

    public float bounds;

    void Update()
    {

        Vector3 mousePos2D = Input.mousePosition;                        

        mousePos2D.z = -Camera.main.transform.position.z;                 

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;

        pos.x = mousePos3D.x;

        if (Mathf.Abs(pos.x) <= bounds)
        {
            this.transform.position = pos;
        }
            

    }

}