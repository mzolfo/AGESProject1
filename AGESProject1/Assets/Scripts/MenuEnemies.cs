using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemies : MonoBehaviour
{
    
    int hasHit;
    public Transform target;
   
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //target =

        //i just need to make a point by which it can 

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        MakeRandomDestination();
    }


    void Update()
    {

        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            nav.enabled = false;
            if (hasHit == 0)
            {
                MakeRandomDestination();
                hasHit = Random.Range(250, 500);
            }
            else
            { hasHit = hasHit - 1; }
        }
        else
        {
            nav.enabled = true;
            nav.SetDestination(target.position);
        }

      

    }

    void MakeRandomDestination()
    {
        Vector3 targetpos = new Vector3(Random.Range(-49.0f, 49.0f), 1.5f, Random.Range(-49.0f, 49.0f));
        target.position = targetpos;
        
    }



}
