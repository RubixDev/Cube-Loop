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
    public int rarity;

    private void Start()
    {
        StartCoroutine(Loop());
    }


    private IEnumerator Loop()
    {
        while (movement.enabled)
        {
            SpawnRow();
            yield return new WaitForSeconds(seconds);
        }
    }

    private void SpawnRow()
    {
        var xPos = -6.4f;
        var range = Random.Range(minObstaclesPerRow, 6);

        var freePos = new Vector3(-6.4f, 1f, zPos);
        var powerUp = Random.Range(0, rarity);
        var powerUpPlaced = false;

        for (var i = 0; i < range; i++)
        {
            var move = Random.Range(0, 2);

            if (move != 0)
            {
                if (move != 1 || i == 0)
                {
                    if (move == 1)
                    {
                        xPos += 2.1f;
                    }
                }
                else
                {
                    xPos += 4.2f;
                }
            }
            else
            {
                if (i != 0)
                {
                    xPos += 2.1f;
                }
            }

            if (xPos > 6.3f)
            {
                break;
            }

            Instantiate(obstacle, new Vector3(xPos, 1, zPos), Quaternion.identity);
        }

        if (powerUp != 0) return;
        for (var n = 0; n < 7; n++)
        {
            var place = Random.Range(0, 2);
            if (Physics.CheckSphere(freePos, radius) == false && powerUpPlaced == false && place == 0)
            {
                Instantiate(bluePowerUp, freePos, Quaternion.identity);
                powerUpPlaced = true;
            }

            freePos.x += 2.1f;
        }

    }

    public void SpawnDeath()
    {
        var xPos = -6.4f;

        const int range = 7;

        for (var i = 0; i < range; i++)
        {
            Instantiate(obstacle, new Vector3(xPos, 3, 10), Quaternion.identity);

            xPos += 2.1f;
        }

        Instantiate(obstacle, new Vector3(0, 3, 5), Quaternion.identity);
    }
}
