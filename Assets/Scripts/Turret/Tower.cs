using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [Header("Turret Stats")]
    public float range = 3f;
    public float turretspeed = 5f;
    private float health;
    public float startHealth;
    public float fireRate = 100f;
    private float fireCountDown = 0f;
    public static bool gameIsOver = false;

    [Header("Turret Logic")]
    private Transform target;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private GameObject impactEffect;
    private bool isDead = false;
    private string enemyTag = "Enemy";

    [Header("Turret UI")]
    public Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //returns if there is no enemy and continues if one is there
        if (target == null)
            return;

        //Calcutaling the distance between the turret and the enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //rotating the turret so it face s the enemy
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    //takes damage and calls and kills turret if hp is 0
    public void TakeDamage(float amount)
    {
        health = health - amount;
        //updates the turrets hp ui when it is damaged
        healthBar.fillAmount = health / startHealth;
        Debug.Log(health);
        //kills turret if it was alive and hp is 0
        if (health <= 0 && isDead == false)
        {
            Die();
        }
    }

    //Logic behind killing the turret
    private void Die()
    {

        Debug.Log("Game Over");
        Time.timeScale = 0.0f;
        Currency.money = 0;
        gameIsOver = true;
        isDead = true;

        Destroy(gameObject);
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

    //updates  which enemy is in range and nearest
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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

    //displays targeting range of turret in scene 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
