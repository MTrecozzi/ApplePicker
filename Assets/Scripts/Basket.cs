using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Basket : MonoBehaviour
{

    public float bounds;

    public PitchScaler pitchScaler = new PitchScaler();

    [Header("Set Dynamically")]
    public TextMeshProUGUI scoreGT;

    public bool canMove = true;

    public void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();

        scoreGT.text = "0";
        
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject collidedWith = col.gameObject;

        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);

             AudioSource source = SFX_II.instance.GetFirstSource("AppleCollect");
            source.pitch = pitchScaler.GetNextPitch();
            source.Play();

            int score = int.Parse(scoreGT.text);

            score += 100;

            scoreGT.text = score.ToString();

            if (score > HighScore.score)
            {
                HighScore.SetScore(score);
            }

        }
    }

    void Update()
    {

        if (canMove)
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

}

public class PitchScaler
{
    public float startingPitch = 0.7f;
    public float pitchInterval = 0.05f;
    public float curPitch = 0.7f;

    public float maxPitch = 2f;

    public float GetNextPitch()
    {
        curPitch += pitchInterval;

        
        if (curPitch > maxPitch)
        {
            curPitch = startingPitch;
        } 

        return curPitch;
    }
}