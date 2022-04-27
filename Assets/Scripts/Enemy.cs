using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    GameManager gameManager;
    Player player;

    NavMeshAgent agent;

    float speed;
    float health;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            transform.localScale = transform.localScale - new Vector3(0.25f, 0, 0.25f);
            health--;
            Destroy(other.gameObject);

            if (health <= 0 )
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        player = GetComponent<Player>();
        health = enemyConfig.health;
        speed = enemyConfig.speed;
        gameManager = GameManager.GetInstance();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(gameManager.player != null)
        agent.SetDestination(gameManager.player.position);
    }
}
