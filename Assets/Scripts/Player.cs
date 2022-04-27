using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public Collider[] enemies;

    public float movingSpeed;
    public float bulletSpeed;
    public float range;
    public float attackSpeed;

    bool isEnemySpotted;
    bool isTouching;
    bool isShooting;
    bool canShot;

    GameObject closestEnemy;
    float distanceBetweenClosestEnemy;

    float count = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        distanceBetweenClosestEnemy = float.MaxValue;
        closestEnemy = null;
    }
    void Update()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        enemies = Physics.OverlapSphere(transform.position, range, enemyLayer, QueryTriggerInteraction.UseGlobal);

        if (enemies.Length >= 1)
        {
            animator.SetBool("isEnemySpotted", true);
            isEnemySpotted = true;

        }

        else
        {
            animator.SetBool("isEnemySpotted", false);
            isEnemySpotted = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = GetMousePosition();
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * movingSpeed);
            animator.SetBool("isTouching", true);
            isTouching = true;

            if (closestEnemy == null) transform.LookAt(pos);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isTouching", false);
            isTouching = false;
        }

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
          
            if (distance < distanceBetweenClosestEnemy)
            {
                distanceBetweenClosestEnemy = distance;
                closestEnemy = enemy.gameObject;
                //transform.LookAt(closestEnemy.transform.position);

                Vector3 lookVector = closestEnemy.transform.position - transform.position;
                lookVector.y = transform.position.y;    
                Quaternion rot = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(transform.localRotation, rot, Time.deltaTime * 20);
            }
        }

        if (isEnemySpotted)
        {
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            isShooting = false;
            StopAllCoroutines();
        }

        distanceBetweenClosestEnemy = float.MaxValue;
    }

    Vector3 GetMousePosition()
    {
        Vector3 clickPosition = -Vector3.one;
        //Check if the ray which is created my mouse click(touch) is hit somewhere
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            clickPosition = hit.point;
        }
        return clickPosition;
    }

    IEnumerator Shoot()
    {
        Debug.Log("Working");
        while (isShooting)
        {
            GameObject temp = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            temp.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            Destroy(temp, 3f);

            yield return new WaitForSeconds(attackSpeed);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, range);
    }
}
