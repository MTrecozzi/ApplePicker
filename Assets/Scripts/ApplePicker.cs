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

    private AppleTree appleTree;

    private Coroutine destroyApples;

    public void Awake()
    {
        appleTree = GameObject.Find("Tree").GetComponent<AppleTree>();
    }

    public void Update()
    {
        
    }


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
        appleTree.IncreaseDifficulty();

        bool finishedGame = false;

        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        List<GameObject> applesList = new List<GameObject>();

        
        foreach (GameObject tGO in tAppleArray)
        {
            //Destroy(tGO);
            applesList.Add(tGO);
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
            finishedGame = true;
        }

        //CallCoroutine
        destroyApples = StartCoroutine(DestroyApples(applesList, finishedGame));
    }

    public IEnumerator DestroyApples(List<GameObject> list, bool finished)
    {

        appleTree.SetActive(false);

        foreach(GameObject obj in list)
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }

        list.Reverse();

        while (list.Count > 0)
        {
            GameObject curApple = list[list.Count - 1];

            list.RemoveAt(list.Count - 1);

            Destroy(curApple);

            yield return new WaitForSeconds(.2f);
        }

        appleTree.SetActive(true);
        appleTree.DropApple();

        if (finished)
        {
            // Finish Game
            SceneManager.LoadScene("_Scene_0");
        }
    }

}
