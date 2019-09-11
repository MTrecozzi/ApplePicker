using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Basket : MonoBehaviour
{

    public float bounds;

    [Header("Set Dynamically")]
    public TextMeshProUGUI scoreGT;

    public void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject collidedWith = col.gameObject;

        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);
        }
    }

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