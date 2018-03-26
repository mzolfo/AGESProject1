using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //this whole mess was a huge waste of time...
    // Renderer myRenderer
    //  Material MyColor;
    //  [SerializeField]
    // Color hitColor = new Color(1f, 0f, 0f, 1f);
    //  [SerializeField]
    //  Color stateColor = new Color(1f, 0.5f, 0.063f, 1f);
    //  [SerializeField]
    // Color CurrentColor;
    // [SerializeField]
    // Color whatsbeinglerpedthen;

    public GameObject TargetPlayer;
    PlayerController targetPlayerStats;

    int hasHit;
    Transform target;
    public float ownHealth = 30f;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        
        targetPlayerStats = TargetPlayer.GetComponent<PlayerController>();
        target = TargetPlayer.transform;
        // myRenderer = GetComponent<Renderer>();
        // MyColor = myRenderer.material;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {

        if (ownHealth <= 0)
        { Destroy(gameObject); }

        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            nav.enabled = false;
            if (hasHit == 0)
            {
                targetPlayerStats.health = targetPlayerStats.health - 5f;
                hasHit = 100;
            }
            else
            { hasHit = hasHit - 1; }
        }
        else {
            nav.enabled = true;
            nav.SetDestination(target.position);
        }

        if (hasHit != 0)
        { hasHit = hasHit - 1; }
        //CurrentColor = MyColor.color;
        //else
        //
        //    nav.enabled = false;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            //  MyColor.color = hitColor;
            //StartCoroutine(HitEffect());
        }
    }


}





























    //  IEnumerator HitEffect()
    // {

    //     while (MyColor.color != stateColor)
    //    {

    //       Color LerpingColor = Color.Lerp(hitColor, stateColor, 100f * Time.deltaTime);
    //    whatsbeinglerpedthen = LerpingColor;
    //     MyColor.color = LerpingColor;
    //     yield return null;
    //  }



    // Color hitColor = new Color(1f, 0f, 0f, 1f);
    //Color stateColor = new Color(1f, 0.5f, 0.063f, 1f);

    // }


    //check if hit on update
    //take damage equal to player's damage rating







//we need to script priority here.
// enemies need to pick whichever player is closest to them at a given time