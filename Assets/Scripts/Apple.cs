using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Set in Inspector")]
    public static float bottomY = -20f;
    public MeshRenderer mesh;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Explode()
    {
        mesh.enabled = false;
        explosion.Play();
        Destroy(transform.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY){
            Destroy(this.gameObject);

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();

            apScript.AppleDestroyed();
        }
    }
}
