using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    
    private int _score;

    public void AddScore(int plusScore)
    {
        _score += plusScore;
    }

    private void Update()
    {
        scoreText.text = _score.ToString(CultureInfo.InvariantCulture);
        var highscore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreText.text = "Highscore: " + highscore;
    }
}
