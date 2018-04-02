using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int owner;
    // public Transform Owner;
    public PlayerController Owner;
    float speed = 8f;
    EnemyMovement damagedEnemy;
   


   
    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stopper") //if it is a stopper die
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy") //if it is an enemy hurt it and die
        {
           damagedEnemy = other.GetComponent<EnemyMovement>();
           damagedEnemy.damaged();
           damagedEnemy.ownHealth = damagedEnemy.ownHealth - 5f;
           Owner.score = Owner.score + 5;
           Destroy(gameObject);
        }
        else { } // otherwise ignore it and keep going
                
    }
}
