using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{

    static public int score = 1000;
    static public TextMeshProUGUI text;

    public void Awake()
    {

        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", score);

        text = this.GetComponent<TextMeshProUGUI>();

        text.text = "High Score: " + score;
    }

    public static void SetScore(int score)
    {
        text.text = "High Score: " + score;

        if (score > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
