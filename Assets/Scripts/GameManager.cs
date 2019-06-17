using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public PlayerMovement movement;
    public Material material;
    public MeshRenderer mesh;
    public SpawnObstacles spawn;
    
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
        mesh.material = material;
        StartCoroutine(StartSpawn());
        Invoke(nameof(Restart), restartDelay);
    }


    private IEnumerator StartSpawn()
    {
        for (var i = 0; i < 50; i++)
        {
            spawn.SpawnDeath();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
