using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject enemy;

    public float spawnDistance;
    public float spawnTime;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 pos;

            int spawnPoint = Random.Range(0, 5);

            switch (spawnPoint)
            {
                case 0:
                    pos = new Vector3(gameManager.player.position.x, 0.82f, gameManager.player.position.z + spawnDistance);
                    break;
                case 1:
                    pos = new Vector3(gameManager.player.position.x + spawnDistance / 2, 0.82f, gameManager.player.position.z + spawnDistance / 2);
                    break;
                case 2:
                    pos = new Vector3(gameManager.player.position.x + spawnDistance, 0.82f, gameManager.player.position.z);
                    break;
                case 3:
                    pos = new Vector3(gameManager.player.position.x + spawnDistance / 2, 0.82f, gameManager.player.position.z - spawnDistance / 2);
                    break;
                case 4:
                    pos = new Vector3(gameManager.player.position.x, 0.82f, gameManager.player.position.z - spawnDistance);
                    break;
              /*  case 5:
                    pos = new Vector3(gameManager.player.position.x - spawnDistance / 2, 0.82f, gameManager.player.position.z - spawnDistance / 2);
                    break;
                case 6:
                    pos = new Vector3(gameManager.player.position.x - spawnDistance, 0.82f, gameManager.player.position.z);
                    break;
                case 7:
                    pos = new Vector3(gameManager.player.position.x - spawnDistance / 2, 0.82f, gameManager.player.position.z + spawnDistance / 2);
                    break;
              */
                default:
                    pos = Vector3.forward;
                    break;
              
            }
            Instantiate(enemy, pos, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
