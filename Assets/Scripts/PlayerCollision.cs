using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager manager;
    public Material defaultMaterial;
    public Material powerUpMaterial;
    public float bluePowerUpSeconds = 10.2f;
    
    private bool _invincible;
    private bool _hasPowerUp;
    private int _loops = 10;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private Transform _transform;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
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
    }

    private IEnumerator BluePowerUp(Component powerUp)
    {
        Destroy(powerUp.gameObject);
        
        if (_hasPowerUp == false)
        {
            _hasPowerUp = true;
            
            _meshRenderer.material = powerUpMaterial;
            _rigidbody.mass *= 1000f;
            _transform.localScale *= 2f;
            
            var position = _transform.position;
            position = new Vector3(position.x, position.y * 1.5f, position.z);
            
            _transform.position = position;
            _invincible = true;

            var seconds = bluePowerUpSeconds / 10;
            
            for (var i = 0; i < _loops; i++)
            {
                yield return new WaitForSeconds(seconds);
            }

            _meshRenderer.material = defaultMaterial;
            _rigidbody.mass /= 1000f;
            _transform.localScale /= 2f;
            
            position = new Vector3(position.x, 1f, position.z);
            
            _transform.position = position;
            _invincible = false;
            _hasPowerUp = false;
        }
        else
        {
            _loops += 10;
        }
    }
}
