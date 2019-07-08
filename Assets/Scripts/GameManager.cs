using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public PlayerMovement movement;
    public Material material;
    public MeshRenderer meshRenderer;
    public SpawnObstacles spawn;
    public bool android;
    
    private bool _gameOver;

    private void Start()
    {
        Cursor.visible = false;
    }

    public void GameOver()
    {
        if (_gameOver) return;
        _gameOver = true;
        movement.enabled = false;
        meshRenderer.material = material;
        StartCoroutine(StartSpawn());
        Time.timeScale = 1f;
        Invoke(nameof(Restart), restartDelay);
    }


    private IEnumerator StartSpawn()
    {
        var timeBetween = 0.05f;
        if (QualitySettings.GetQualityLevel() == 2 && android |
            QualitySettings.GetQualityLevel() == 0 && android == false)
        {
            timeBetween = 0.2f;
        }
        
        for (var i = 0; i < 50; i++)
        {
            spawn.SpawnDeath();
            yield return new WaitForSeconds(timeBetween);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
