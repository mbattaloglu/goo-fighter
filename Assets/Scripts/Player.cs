using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    public GameObject bullet;

    public float speed;
    public float radius;

    bool isEnemySpotted;
    bool isTouching;
    bool isShooting;

    float x1, y1, x2, y2;

    GameObject closestEnemy;
    float distanceBetweenClosestEnemy;

    Transform touchingBulletPoint;
    Transform idleBulletPoint;

    private void Start()
    {
        touchingBulletPoint = transform.GetChild(2);
        idleBulletPoint = transform.GetChild(3);
        animator = GetComponent<Animator>();
        distanceBetweenClosestEnemy = float.MaxValue;
        closestEnemy = null;
    }
    void Update()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer, QueryTriggerInteraction.UseGlobal);
        Debug.Log(enemies.Length);

        if (enemies.Length >= 1)
        {
            isEnemySpotted = true;
            animator.SetBool("isEnemySpotted", true);
        }

        else
        {
            animator.SetBool("isEnemySpotted", false);
            isEnemySpotted = false;
        }



        if (Input.GetMouseButton(0))
        {
            Vector3 pos = GetMousePosition();
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
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
                transform.LookAt(closestEnemy.transform.position);
            }

            isEnemySpotted = true;
        }
        distanceBetweenClosestEnemy = float.MaxValue;

        if (isEnemySpotted)
        {
            if (!isShooting)
            {
                StartCoroutine(Shoot());
                isShooting = true;
            }
        }

        else
        {
            StopCoroutine(Shoot());
            isShooting = false;
        }
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
        while (true)
        {
            if (isTouching)
            {
                GameObject temp = Instantiate(bullet, touchingBulletPoint.position, Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(closestEnemy.transform.position * Time.deltaTime * 2500);
            }
            else
            {
                GameObject temp = Instantiate(bullet, idleBulletPoint.position, Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(closestEnemy.transform.position * Time.deltaTime * 2500);
            }
            yield return new WaitForSeconds(0.7f);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
