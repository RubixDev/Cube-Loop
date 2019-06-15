using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject prefab;
    public float seconds = 4f;
    public PlayerMovement movement;
    public float zPos = 100f;
    public int minObstaclesPerRow = 4;

    void Start()
    {
        StartCoroutine(Wait());
    }

    
    IEnumerator Wait()
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
        
        for (int i = 0; i < range; i++)
        {
            int move = Random.Range(0, 2);

            if (move == 1)
            {
                if (i != 0)
                {
                    xPos += 2.1f;
                }
            }
            else
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

            if (xPos > 6.3f)
            {
                break;
            }
            
            Instantiate(prefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        }

    }

    public void SpawnDeath()
    {
        float xPos = -6.4f;

        int range = 7;

        for (int i = 0; i < range; i++)
        {
            Instantiate(prefab, new Vector3(xPos, 3, 10), Quaternion.identity);

            xPos += 2.1f;
        }

        Instantiate(prefab, new Vector3(0, 3, 5), Quaternion.identity);
    }
}
