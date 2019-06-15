﻿using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public PlayerMovement movement;
    public float delay = 0.3f;
    public GameObject spawner;

    float _seconds;
    int _score;


    void Start()
    {
        _seconds = spawner.GetComponent<SpawnObstacles>().seconds;
        
        StartCoroutine(SetScore());
    }


    IEnumerator SetScore()
    {
        yield return new WaitForSeconds(delay);
        
        while (movement.enabled)
        {
            _score += 1;
            if (_score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", _score);
            }
            yield return new WaitForSeconds(_seconds);
        }
    }


    void Update()
    {
        scoreText.text = _score.ToString(CultureInfo.InvariantCulture);
        string highscore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreText.text = "Highscore: " + highscore;
    }
}
