using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager manager;
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            manager.GameOver();
        }
    }
}
