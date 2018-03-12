using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour 
{
    [SerializeField]
    Vector3 enemyBoundTo;
    
    public Transform cameraPosition;
    GameObject Camera;
   

    private void Awake()
    {
       Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Use this for initialization
    void Start () 
	{
       
       
	}
	
	// Update is called once per frame
	void Update () 
	{
        enemyBoundTo = GetComponentInParent<Transform>().position;
        cameraPosition = Camera.transform;
        FindNewPosition();

    }

    void FindNewPosition()
    {
        transform.position = new Vector3(enemyBoundTo.x, enemyBoundTo.y + 1, enemyBoundTo.z - 1);
    }

    void FindNewRotation()
    {

    }
}


//trying to interpolate this canvas to always be facing the camera.
//first find the two positions to be interpolating between
