using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemies : MonoBehaviour
{

	
    

   

    int hasHit;
    Transform target;
   
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //target =



        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {

        

        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            nav.enabled = false;
        }
        else
        {
            nav.enabled = true;
            nav.SetDestination(target.position);
        }
        
    }

    void MakeRandomDestination()
    {
        Vector3 target = new Vector3(Random.Range(-49.0f, 49.0f), 0, Random.Range(-49.0f, 49.0f));
        float targetPointIndexX = Random.Range(0, 50);
    }



}
