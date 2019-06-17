using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text highScoreText;

    private void Start()
    {
        var highscore = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreText.text = "Highscore: " + highscore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene(1);
    }
}
