using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float slowSpeed;
   [SerializeField] public bool isSlowing; 
    public float damage;
    private Transform target;
    public float speed = 5f;
    public float explosionRadius = 0;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return;
        }

        //calculating the distance between the enemy and bullet
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        if(explosionRadius > 0)
        {
            Explode();
        }
        if (isSlowing)
        {
            Slow();
        }
        else
        {
            TurretDamage(target);
            EnemyDamage(target);
            TowerDamage(target);
            Destroy(gameObject);
        }
        
       
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                EnemyDamage(collider.transform);
                Destroy(gameObject);
            }
        }
    }
    void Slow()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                EnemySlow(collider.transform);
                Destroy(gameObject);
            }
        }
    }
    void EnemySlow(Transform enemy)
    {
        EnemyTargets e = enemy.GetComponent<EnemyTargets>();
        if (e != null)
        {
            e.TakeSlow(slowSpeed);
        }
    }
    //getting the Tranform of the enemy shot  
    void EnemyDamage(Transform enemy)
    {
        //assigning the enemies Transform to a var
        EnemyTargets e = enemy.GetComponent<EnemyTargets>();
        //it only runs if there is an enemy
        if (e != null)
        {
            e.TakeDamage(damage);
        }
      
        
    }

    //getting the Tranform of the turret shot 
    void TurretDamage(Transform turret)
    {
        //assigning the turret Transform to a var
        Turret t = turret.GetComponent<Turret>();
        //it only runs if there is an turret
        if (t != null)
        {
            t.TakeDamage(damage);
        }


    }
    void TowerDamage(Transform tower)
    {
        //assigning the turret Transform to a var
        Tower t = tower.GetComponent<Tower>();
        //it only runs if there is an turret
        if (t != null)
        {
            t.TakeDamage(damage);
        }


    }
}
