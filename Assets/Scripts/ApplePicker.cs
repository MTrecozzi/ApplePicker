using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]                                             // a
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketTopY = -10f;
    public float basketSpacingY = 2f;

    public List<GameObject> baskets;


    void Start()
    {

        baskets = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketTopY - (basketSpacingY * i);
            tBasketGO.transform.position = pos;

            baskets.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");

        AppleTree currentAppleTree = GameObject.Find("Tree").GetComponent<AppleTree>();

        currentAppleTree.IncreaseDifficulty();

        // replace with coroutine;
        foreach (GameObject tGO in tAppleArray)
        {
            Destroy(tGO);
        }

        int basketIndex = baskets.Count - 1;

        if (basketIndex >= 0)
        {
            GameObject tBasketGameObject = baskets[basketIndex];

            baskets.RemoveAt(basketIndex);

            Destroy(tBasketGameObject);
        }

        Debug.Log(baskets.Count);

        if (baskets.Count == 0)
        {
            // Finish Game
            SceneManager.LoadScene("_Scene_0");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
