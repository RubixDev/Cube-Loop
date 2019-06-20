using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager manager;
    public Material defaultMaterial;
    public Material bluePowerUpMaterial;
    public Material greenPowerUpMaterial;
    public float powerUpSeconds = 10.2f;
    [HideInInspector]
    public bool hasGreenPowerUp;
    
    private bool _invincible;
    private bool _hasBluePowerUp;
    private int _blueLoops = 10;
    private int _greenLoops = 10;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private Transform _transform;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _transform = transform;
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
        }
        if(other.gameObject.CompareTag("GreenPowerUp"))
        {
            StartCoroutine(GreenPowerUp(other));
        }
    }

    private IEnumerator BluePowerUp(Component powerUp)
    {
        Destroy(powerUp.gameObject);
        
        if (_hasBluePowerUp == false)
        {
            _hasBluePowerUp = true;
            
            _meshRenderer.material = bluePowerUpMaterial;
            _rigidbody.mass *= 10000f;
            var scale = _transform.localScale;
            _transform.localScale = new Vector3(2f, scale.y, scale.z);
            _invincible = true;

            var seconds = powerUpSeconds / 10;
            
            for (var i = 0; i < _blueLoops; i++)
            {
                yield return new WaitForSeconds(seconds);
            }

            _meshRenderer.material = hasGreenPowerUp == false ? defaultMaterial : greenPowerUpMaterial;
            _rigidbody.mass /= 1000f;
            _transform.localScale = new Vector3(1f, scale.y, scale.z);
            _invincible = false;
            _hasBluePowerUp = false;
        }
        else
        {
            _blueLoops += 10;
        }
    }

    private IEnumerator GreenPowerUp(Component powerUp)
    {
        Destroy(powerUp.gameObject);
        
        if (hasGreenPowerUp == false)
        {
            hasGreenPowerUp = true;
            _meshRenderer.material = greenPowerUpMaterial;
            Time.timeScale = 0.5f;
            
            var seconds = powerUpSeconds / 10;
            
            for (var i = 0; i < _greenLoops; i++)
            {
                yield return new WaitForSeconds(seconds);
            }
            
            hasGreenPowerUp = false;
            _meshRenderer.material = _hasBluePowerUp == false ? defaultMaterial : bluePowerUpMaterial;
            Time.timeScale = 1f;
        }
        else
        {
            _greenLoops += 10;
        }
    }
}
