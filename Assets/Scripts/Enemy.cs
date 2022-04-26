using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    GameManager gameManager;

    float speed;
    float health;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        health = enemyConfig.health;
        speed = enemyConfig.speed;
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        Vector3 pos = new Vector3(gameManager.player.position.x, transform.position.y, gameManager.player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
