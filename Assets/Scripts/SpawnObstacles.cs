using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject bluePowerUp;
    public float seconds = 4f;
    public PlayerMovement movement;
    public float zPos = 100f;
    public int minObstaclesPerRow = 4;
    public float radius = 1f;

    void Start()
    {
        StartCoroutine(Loop());
    }

    
    IEnumerator Loop()
    {
        while (movement.enabled)
        {
            SpawnRow();
            yield return new WaitForSeconds(seconds);
        }
    }

    void SpawnRow()
    {
        float xPos = -6.4f;
        int range = Random.Range(minObstaclesPerRow, 6);

        Vector3 freePos = new Vector3(-6.4f, 1f, zPos);
        int powerUp = Random.Range(0, 1);
        bool powerUpPlaced = false;

        for (int i = 0; i < range; i++)
        {
            int move = Random.Range(0, 3);

            if (move == 0)
            {
                if (i != 0)
                {
                    xPos += 2.1f;
                }
            }
            else if (move == 1)
            {
                if (i != 0)
                {
                    xPos += 4.2f;
                }
                else
                {
                    xPos += 2.1f;
                }
            }
            else
            {
                Instantiate(bluePowerUp, freePos, Quaternion.identity);
            }

            if (xPos > 6.3f)
            {
                break;
            }

            Instantiate(obstacle, new Vector3(xPos, 1, zPos), Quaternion.identity);
        }

        if (powerUp == 0)
        {
            for (int n = 0; n < 7; n++)
            {
                int place = Random.Range(0, 2);
                if (Physics.CheckSphere(freePos, radius) == false && powerUpPlaced == false && place == 0)
                {
                    Instantiate(bluePowerUp, freePos, Quaternion.identity);
                    powerUpPlaced = true;
                }

                freePos.x += 2.1f;
            }
        }

    }

    public void SpawnDeath()
    {
        float xPos = -6.4f;

        int range = 7;

        for (int i = 0; i < range; i++)
        {
            Instantiate(obstacle, new Vector3(xPos, 3, 10), Quaternion.identity);

            xPos += 2.1f;
        }

        Instantiate(obstacle, new Vector3(0, 3, 5), Quaternion.identity);
    }
}
