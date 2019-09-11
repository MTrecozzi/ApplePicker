using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set In Inspector")]
    [Space(10)]
    public float leftAndRightEdge = 10f;
    public GameObject applePrefab;

    [Header ("Starting Difficulty Settigns")]
    public float speed = 1f;
    public float chanceToChangeDirections = 0.1f;
    public float secondsBetweenAppleDrops = 3f;

    [Header ("Current Difficulty Info")]
    public DifficultyField currentDifficulty;

    [Header("GamePlay")]
    public float gameTime = 0f;
    public float difficultyTimeInterval;
    public float nextDifficultySpike;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        currentDifficulty = new DifficultyField(speed, chanceToChangeDirections, secondsBetweenAppleDrops);
        Invoke("DropApple", currentDifficulty.secondsBetweenAppleDrops);
        nextDifficultySpike = gameTime + difficultyTimeInterval;
    }

    public void SetActive(bool _active)
    {
        this.active = _active;
    }

    public void IncreaseDifficulty()
    {
        currentDifficulty.AddDifficulty(1);
        this.speed = currentDifficulty.speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (active)
        {
            gameTime += Time.deltaTime;
            if (gameTime >= nextDifficultySpike)
            {
                nextDifficultySpike += difficultyTimeInterval;
                IncreaseDifficulty();
            }

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

        
    }

    public void DropApple()
    {                                                  // b
        if (active)
        {
            GameObject apple = Instantiate<GameObject>(applePrefab);      // c

            apple.transform.position = transform.position;                  // d

            Invoke("DropApple", currentDifficulty.secondsBetweenAppleDrops);                // e

        }

    }

    void FixedUpdate()
    {
        if (Random.value < currentDifficulty.chanceToChangeDirections)
        {        // a
            speed *= -1; // Change direction                           // b
        }
    }
}

[System.Serializable]
public class DifficultyField
{
    public float speed = 15f;
    public float chanceToChangeDirections = 0.02f;
    public float secondsBetweenAppleDrops = 0.3f;


    public DifficultyField(float _speed = 15f, float _chanceToChange = 0.02f, float _secondsBtwnDrops = 0.3f)
    {
        this.speed = _speed;
        this.chanceToChangeDirections = _chanceToChange;
        this.secondsBetweenAppleDrops = _secondsBtwnDrops;
    }

    public void AddDifficulty(int difficultyToAdd)
    {
        this.speed += difficultyToAdd * 0.5f;
        this.chanceToChangeDirections += 0.002f * difficultyToAdd;
        this.secondsBetweenAppleDrops -= 0.05f * difficultyToAdd;

        if (this.secondsBetweenAppleDrops < 0.1f)
        {
            this.secondsBetweenAppleDrops = 0.1f;
        }
    }
}


