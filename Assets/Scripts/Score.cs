using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    [HideInInspector]
    public int score;

    public void AddScore(int plusScore)
    {
        score += plusScore;

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
        
        var highscore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreText.text = "Highscore: " + highscore;
    }
}
