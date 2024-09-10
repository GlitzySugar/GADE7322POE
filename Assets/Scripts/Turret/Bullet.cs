using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    [SerializeField] private GameObject impactEffect;

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

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(target.gameObject);
        GameObject  effectInsatance = Instantiate(impactEffect, target.position, transform.rotation);
        Destroy(effectInsatance, 2f);
        Debug.Log("Hit Something");
    }
}
