using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectionTransform;
    //movement variables
    public float speed = 6f;
    public Text scoreText;
    public Text healthText;
    public Text damageText;


    Rigidbody playerRigidbody;
    [SerializeField]
    float currentx;
    [SerializeField]
    float currenty;
    [SerializeField]
    float currentshootx;
    [SerializeField]
    float currentshooty;

    //health and damage variables
    [SerializeField]
    public float health = 100f;
    public float damage = 5f;
    public float score = 0f;

    //state variables
    [SerializeField]
    public int PlayerNumber;
    [SerializeField]
    public bool isDead = false;
    int recentlyShot;

    void Awake()
    {


        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        damageText.text = "Damage: " + damage;
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            isDead = true;
        }

        //just testing to see how the inputs are doing in the engine window.
        currentx = Input.GetAxis("MoveHorizontal" + PlayerNumber);
        currenty = Input.GetAxis("MoveVertical" + PlayerNumber);
        currentshootx = Input.GetAxis("ShootHorizontal" + PlayerNumber);
        currentshooty = Input.GetAxis("ShootVertical" + PlayerNumber);


        if (recentlyShot != 0)
        {
            recentlyShot = recentlyShot - 1;
        }

        if (currentshootx != 0 || currentshooty != 0)
        {
            if (recentlyShot == 0)
            {
                Shoot();
            }
            //fire a projectile and wait a second to fire another.
        }

        Move();
        turn();
        if (isDead)
        {
            Die();
        }
    }


    void Move()
    {

        //calculate the speed and distance the player should be moving according to their inputs
        float h = Input.GetAxis("MoveHorizontal" + PlayerNumber) * speed * Time.deltaTime;
        float v = (Input.GetAxis("MoveVertical" + PlayerNumber) * -1) * speed * Time.deltaTime;
        Vector3 moveVec = new Vector3(h, 0, v);
        transform.Translate(moveVec, Space.World);
    }

    void turn()
    {
        if (Input.GetAxis("ShootHorizontal" + PlayerNumber) == 0 && Input.GetAxis("ShootVertical" + PlayerNumber) == 0)
        {

        }
        else {
            Vector3 lookVec = new Vector3(Input.GetAxis("ShootHorizontal" + PlayerNumber), 0, (Input.GetAxis("ShootVertical" + PlayerNumber) * -1));

            transform.rotation = Quaternion.LookRotation(lookVec, Vector3.up);
        }

    }

    void Shoot()
    {
        GameObject ProjectileInstance = Instantiate(projectile, projectionTransform.position, projectionTransform.rotation) as GameObject;
        Projectile shotscript = ProjectileInstance.GetComponent<Projectile>();
        shotscript.owner = PlayerNumber;
        shotscript.Owner = GetComponent<PlayerController>();
        recentlyShot = 5;
    }


    void Die() //unfinished unused
    {
        healthText.text = "Dead";
        gameObject.SetActive(false);
        //probably want to play a sound then pass to playermanager that it needs to be deactivated again.
    }
}







/*
 so what do we want for functionality here. Every controller needs its right and left control sticks.
 It would be nice to also have the dpad for any of those. 
 we need the a button to work for progression and join state
 probably want that start button as well.
     */







