using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{   
    private NavMeshAgent enemy;
    public Transform tower;

    // Start is called before the first frame update
    void Start()
    {
       enemy =  GetComponent<NavMeshAgent>();
       enemy.destination = new Vector3(14.4f, 1.5f, 14.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
