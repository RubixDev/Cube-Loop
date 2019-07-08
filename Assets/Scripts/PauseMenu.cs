using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject gameManager;
    
    private static bool _gameIsPaused;
    private bool _android;
    private PlayerCollision _playerCollision;

    private void Start()
    {
        _gameIsPaused = false;
        var player = GameObject.FindGameObjectWithTag("Player");
        _playerCollision = player.GetComponent<PlayerCollision>();

        _android = gameManager.GetComponent<GameManager>().android;
    }

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
        if (_playerCollision.hasGreenPowerUp)
        {
            Time.timeScale = 0.7f;
        }
        _gameIsPaused = false;

        if (_android)
        {
            pauseButton.SetActive(true);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        _gameIsPaused = true;
        
        if (_android)
        {
            pauseButton.SetActive(false);
        }
    }
    
    public void RestartButton()
    {
        Time.timeScale = 1f;
        _gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        Time.timeScale = 1f;
        _gameIsPaused = false;
        SceneManager.LoadScene(1);
    }
}
