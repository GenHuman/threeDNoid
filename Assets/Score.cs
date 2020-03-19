using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public int score = 0;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int addScore(int value)
    {
        score += value;
        ScoreText.text = score.ToString();
        return score;
    }
}
