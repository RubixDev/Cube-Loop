using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager manager;
    public GameObject playerModel;
    public BoxCollider playerCollider;
    
    [Header("Default Skin")]
    public Mesh defaultDefaultMesh;
    public Mesh defaultGreenPowerUpMesh;
    public Mesh defaultBluePowerUpMesh;
    public Mesh defaultBothPowerUpsMesh;
    
    [Header("Maze Skin")]
    public Mesh mazeDefaultMesh;
    public Mesh mazeGreenPowerUpMesh;
    public Mesh mazeBluePowerUpMesh;
    public Mesh mazeBothPowerUpsMesh;
    
    [Header("Egypt Skin")]
    public Mesh egyptDefaultMesh;
    public Mesh egyptGreenPowerUpMesh;
    public Mesh egyptBluePowerUpMesh;
    public Mesh egyptBothPowerUpsMesh;
    
    [HideInInspector]
    public bool hasGreenPowerUp;
    
    private bool _invincible;
    private bool _hasBluePowerUp;
    private bool _blueExpired;
    private bool _greenExpired;
    private int _score;
    private int _currentScoreBlue = -1;
    private int _currentScoreGreen = -1;
    private Mesh _defaultMesh;
    private Mesh _greenPowerUpMesh;
    private Mesh _bluePowerUpMesh;
    private Mesh _bothPowerUpsMesh;
    private Rigidbody _rigidbody;
    private MeshFilter _meshFilter;
    private Score _scoreScript;

    private void Start()
    {
        _scoreScript = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Score>();
        _meshFilter = playerModel.GetComponent<MeshFilter>();
        _rigidbody = GetComponent<Rigidbody>();
        
        setSkin();

        _meshFilter.mesh = _defaultMesh;
    }

    private void Update()
    {
        _score = _scoreScript.score;

        if (_score == _currentScoreBlue + 10 && _currentScoreBlue != -1)
        {
            _blueExpired = true;
        }
        else
        {
            _blueExpired = false;
        }
        if (_score == _currentScoreGreen + 10 && _currentScoreGreen != -1)
        {
            _greenExpired = true;
        }
        else
        {
            _greenExpired = false;
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Obstacle") && _invincible == false)
        {
            manager.GameOver();
        }
        else if (collisionInfo.collider.CompareTag("Obstacle") && _invincible)
        {
            Destroy(collisionInfo.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BluePowerUp"))
        {
            StartCoroutine(BluePowerUp(other));
            // BluePowerUp(other);
        }
        if(other.gameObject.CompareTag("GreenPowerUp"))
        {
            StartCoroutine(GreenPowerUp(other));
        }
    }

    private void setSkin()
    {
        if (PlayerPrefs.GetInt("Skin", 0) == 0)
        {
            _defaultMesh = defaultDefaultMesh;
            _greenPowerUpMesh = defaultGreenPowerUpMesh;
            _bluePowerUpMesh = defaultBluePowerUpMesh;
            _bothPowerUpsMesh = defaultBothPowerUpsMesh;
        }
        else if (PlayerPrefs.GetInt("Skin", 0) == 1)
        {
            _defaultMesh = mazeDefaultMesh;
            _greenPowerUpMesh = mazeGreenPowerUpMesh;
            _bluePowerUpMesh = mazeBluePowerUpMesh;
            _bothPowerUpsMesh = mazeBothPowerUpsMesh;
        }
        else if (PlayerPrefs.GetInt("Skin", 0) == 2)
        {
            _defaultMesh = egyptDefaultMesh;
            _greenPowerUpMesh = egyptGreenPowerUpMesh;
            _bluePowerUpMesh = egyptBluePowerUpMesh;
            _bothPowerUpsMesh = egyptBothPowerUpsMesh;
        }
    }
    
    private IEnumerator BluePowerUp(Component powerUp)
    {
        Destroy(powerUp.gameObject);
        
        if (_hasBluePowerUp == false)
        {
            _hasBluePowerUp = true;
            
            _meshFilter.mesh = hasGreenPowerUp == false ? _bluePowerUpMesh : _bothPowerUpsMesh;
            _rigidbody.mass *= 10000f;
            var size = playerCollider.size;
            playerCollider.size = new Vector3(2f, size.y, size.z);
            _invincible = true;

            _currentScoreBlue = _score;

            while (!_blueExpired)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            
            _meshFilter.mesh = hasGreenPowerUp == false ? _defaultMesh : _greenPowerUpMesh;
            _rigidbody.mass /= 1000f;
            playerCollider.size = new Vector3(1f, size.y, size.z);
            _invincible = false;
            _hasBluePowerUp = false;
        }
        else
        {
            _currentScoreBlue += 10;
        }
    }

    private IEnumerator GreenPowerUp(Component powerUp)
    {
        Destroy(powerUp.gameObject);
        
        if (hasGreenPowerUp == false)
        {
            hasGreenPowerUp = true;
            _meshFilter.mesh = _hasBluePowerUp == false ? _greenPowerUpMesh : _bothPowerUpsMesh;
            Time.timeScale = 0.7f;
            
            _currentScoreGreen = _score;

            while (!_greenExpired)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            
            hasGreenPowerUp = false;
            _meshFilter.mesh = _hasBluePowerUp == false ? _defaultMesh : _bluePowerUpMesh;
            Time.timeScale = 1f;
        }
        else
        {
            _currentScoreGreen += 10;
        }
    }
}
