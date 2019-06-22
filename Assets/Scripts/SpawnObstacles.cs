using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject scoreCounter;
    public GameObject bluePowerUp;
    public GameObject greenPowerUp;
    public float seconds = 4f;
    public PlayerMovement movement;
    public float zPos = 100f;
    public int minObstaclesPerRow = 4;
    public float radius = 1f;
    public int blueRarity;
    public int greenRarity;

    private void Start()
    {
        StartCoroutine(Loop());
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        while (movement.enabled && seconds > 0.85f)
        {
            yield return new WaitForSeconds(2f);
            seconds -= 0.05f;
        }
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
        var placeBluePowerUp = Random.Range(0, blueRarity);
        var placeGreenPowerUp = Random.Range(0, greenRarity);
        var bluePowerUpPlaced = false;
        var greenPowerUpPlaced = false;
        
        Instantiate(scoreCounter, new Vector3(0, -1, zPos), Quaternion.identity);

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

        if (placeBluePowerUp == 0)
        {
            for (var n = 0; n < 7; n++)
            {
                var place = Random.Range(0, 2);
                if (Physics.CheckSphere(freePos, radius) == false && bluePowerUpPlaced == false && place == 0)
                {
                    Instantiate(bluePowerUp, freePos, Quaternion.identity);
                    bluePowerUpPlaced = true;
                }

                freePos.x += 2.1f;
            }
        }

        freePos = new Vector3(-6.4f, 1f, zPos);

        if (placeGreenPowerUp == 0)
        {
            for (var n = 0; n < 7; n++)
            {
                var place = Random.Range(0, 2);
                if (Physics.CheckSphere(freePos, radius) == false && greenPowerUpPlaced == false && place == 0)
                {
                    Instantiate(greenPowerUp, freePos, Quaternion.identity);
                    greenPowerUpPlaced = true;
                }

                freePos.x += 2.1f;
            }
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
