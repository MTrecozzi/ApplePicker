using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set In Inspector")]
    [Space(10)]
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.1f;

    public float secondsBetweenAppleDrops = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropApple", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement Spec
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;

        transform.position = pos;

        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    void DropApple()
    {                                                  // b

        GameObject apple = Instantiate<GameObject>(applePrefab);      // c

        apple.transform.position = transform.position;                  // d

        Invoke("DropApple", secondsBetweenAppleDrops);                // e

    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {        // a
            speed *= -1; // Change direction                           // b
        }
    }
}
