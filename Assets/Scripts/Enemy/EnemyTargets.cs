using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyTargets : MonoBehaviour
{
    [Header("Enemy Stats")]
    private float health;
    private float startHealth = 100f;
    private int worth = 50;
    public float range = 3f;
    public float enemyShootingspeed = 5f;
    public float fireRate = 100f;
    public static float score;
    private float fireCountDown = 0f;

    [Header("Enemy Logic")]
    private Renderer rend;
    private Color startColor;
    public GameObject EnemyBullet;
    private bool isDead = false;
    private Transform target;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private string turretTag = "Turret";
    private string TowerTag = "Tower";
    [SerializeField] private GameObject impactEffect;

    [Header("Enemy UI")]
    public Image healthBar;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        health = startHealth;
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        InvokeRepeating("UpdateTowerTarget", 0, 0.5f);
    }

    private void Update()
    {
        //returns if there is no turret targets and continues if one is there
        if (target == null)
            return;

        //Calcutaling the distance between the turret and the enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * enemyShootingspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }
    //takes damage and calls and kills enemy if hp is 0
    public void TakeDamage(float amount )
    {
        health = health - amount;
        //updates the enemy hp ui when it is damaged
        healthBar.fillAmount = health / startHealth;
        Debug.Log(health);
        //kills enemy if it was alive and hp is 0
        if ( health <= 0 && isDead ==false)
        {
            Die();
        }
    }

    //Logic behind killing the enemy
    private void Die()
    {
        GameObject effectInsatance = Instantiate(impactEffect, this.gameObject.transform.position, transform.rotation);
        Destroy(effectInsatance, 2f);
        isDead = true;
        score += 1;
        Currency.money += worth;
        Destroy( gameObject );
    }

    void Shoot()
    {
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
    //updates  which turret is in range and nearest so it can target the closest
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(turretTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance > distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    private void UpdateTowerTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TowerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance > distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    //displays targeting range of enemy in scene 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
