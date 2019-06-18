using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    
    private static bool _gameIsPaused;

    private void Update()
    {
        if (!Input.GetButtonDown("Escape")) return;
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
        _gameIsPaused = false;
        pauseButton.SetActive(true);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        _gameIsPaused = true;
        pauseButton.SetActive(false);
    }
    
    public void RestartButton()
    {
        _gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        _gameIsPaused = false;
        SceneManager.LoadScene(0);
    }
    
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SettingsButton()
    {
        _gameIsPaused = false;
        SceneManager.LoadScene(1);
    }
}
