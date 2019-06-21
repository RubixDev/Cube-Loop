using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2000f;

    private Score _score;
    private bool _scoreAdded;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        var scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        _score = scoreText.GetComponent<Score>();

        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("AtPlayer") && _playerMovement.enabled)
        {
            _score.AddScore(1);
        }
    }
}
