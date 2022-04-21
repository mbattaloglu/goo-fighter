using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyConfig enemyConfig;
    GameManager gameManager;

    public float speed;
    float health;

    private void Start()
    {
        health = enemyConfig.health;
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, gameManager.player.position, Time.deltaTime * speed);
    }
}
