using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {



    GameObject checkedPlayer;
    GameObject cameraRig;
    CameraController CurrentCamera;
    [SerializeField]
    List<GameObject> Players;
    [SerializeField]
    List<GameObject> ActivePlayers;
    [SerializeField]
    List<GameObject> PlayerUIs;
    [SerializeField]
    GameObject CheckedUI;

    public GameObject InstructText;
    [SerializeField]
    bool joinstate = false;
    [SerializeField]
    bool beginGame = false;
    [SerializeField]
    int numPlayers;
    [SerializeField]
    int playersStarting;
    [SerializeField]
    bool p1In;
    [SerializeField]
    bool p2In;
    [SerializeField]
    bool p3In;
    [SerializeField]
    bool p4In;

    int WaveRate = 0;



    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField]
    float TargetedDist = 0f;
    [SerializeField]
    GameObject trgetedPlayer;
    [SerializeField]
    int chosenspawn;

   

private void Awake() //if you need this script to find things do it in awake
    {

        cameraRig = GameObject.Find("CameraRig");//collect the camera rig so we can later expand its targets
        

        Players = new List<GameObject>(); //collect every player that could be in the game ready to be played
        ActivePlayers = new List<GameObject>();
        //PlayerUIs = new List<GameObject>();
        
        for (int i = 1; i <= 4; i++) // add them to a list so no one needs to find them again.
        {
            checkedPlayer = GameObject.Find("Player" + i);
            Players.Add(checkedPlayer);

        }

        for (int i = 0; i < 4; i++) 
        {
            //CheckedUI = GameObject.Find("Player" + i + "UI");
            //PlayerUIs.Add(CheckedUI);
            PlayerUIs[i].SetActive(false);        
        }
     }

    // Use this for initialization
    void Start()
    { 
        CurrentCamera = cameraRig.GetComponent<CameraController>();
        foreach (GameObject p in Players)
        { p.SetActive(false); } //deactivate all players as they are prepared to be played with when assigned.
        joinstate = true; //enter joinstate right as we begin.
        
    }

    // Update is called once per frame
    void Update()
    {
        if (joinstate)
        {
            JoinGameCheck();
            StartRequestCheck();
        }
        else if (beginGame)
        {
            OpenPlayerUI();
            beginGame = false;
        }
        else
        {
            checkisdead(); ///this is calling the player that has deactivated itself to tell the controller whether it is dead.
        }
       
    
	}

    private void FixedUpdate()
    {
        if (!joinstate)
        {
            if (WaveRate == 0)
            {
                Spawn();
                WaveRate = 30;
            }
            else { WaveRate = WaveRate - 1; }
        }
    }

    void JoinGameCheck()
    {
        if (Input.GetKeyDown("joystick 1 button 0") && !p1In) //check if the player who is holding controller 1 has pressed a and if they have not already joined.
        {
            ActivePlayers.Add(Players[0]);
            Players[0].SetActive(true);
            numPlayers = numPlayers + 1;
            CurrentCamera.Targets.Add(Players[0].GetComponent<Transform>());
            p1In = true;
        }
        if (Input.GetKeyDown("joystick 2 button 0") && !p2In)
        {
            ActivePlayers.Add(Players[1]);
            Players[1].SetActive(true);
            numPlayers = numPlayers + 1;
            CurrentCamera.Targets.Add(Players[1].GetComponent<Transform>());
            p2In = true;
        }
        if (Input.GetKeyDown("joystick 3 button 0") && !p3In)
        {
            ActivePlayers.Add(Players[2]);
            Players[2].SetActive(true);
            numPlayers = numPlayers + 1;
            CurrentCamera.Targets.Add(Players[2].GetComponent<Transform>());
            p3In = true;
        }
        if (Input.GetKeyDown("joystick 4 button 0") && !p4In)
        {
            ActivePlayers.Add(Players[3]);
            Players[3].SetActive(true);
            numPlayers = numPlayers + 1;
            CurrentCamera.Targets.Add(Players[3].GetComponent<Transform>());
            p4In = true;
        }
    }

    void StartRequestCheck()
    {
        if (Input.GetKey("joystick 1 button 7") && p1In)
        {
            playersStarting = playersStarting + 1;
        }
        if (Input.GetKey("joystick 2 button 7") && p2In)
        {
            playersStarting = playersStarting + 1;
        }
        if (Input.GetKey("joystick 3 button 7") && p3In)
        {
            playersStarting = playersStarting + 1;
        }
        if (Input.GetKey("joystick 4 button 7") && p4In)
        {
            playersStarting = playersStarting + 1;
        }

        if (playersStarting == numPlayers && playersStarting != 0)
        {
            joinstate = false;
            beginGame = true;
        }
        else { playersStarting = 0; }
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        chosenspawn = spawnPointIndex;
        GameObject targetedPlayer = null;
        float targetdist = 1000000000;
        foreach (GameObject Player in ActivePlayers) //the intention here is to find each player's location and set the one closest to the instantiated enemy to be its target
        { // I don't know why it doesnt work that way but it seems to split the aggro pretty well anyways. Somehow.

            float checkdist = Vector3.Distance(spawnPoints[spawnPointIndex].position, Player.transform.position);
            if (checkdist < targetdist)
            {
                targetdist = checkdist;
                targetedPlayer = Player;
                TargetedDist = targetdist;
                trgetedPlayer = targetedPlayer;
            }

        }
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyMovement spawnedScript = enemy.GetComponent<EnemyMovement>();
        if (targetedPlayer != null)
        {
            spawnedScript.TargetPlayer = targetedPlayer;
        }
        else
        { print("ERROR: NO PLAYER WAS FOUND"); }

    }

    void OpenPlayerUI()
    {
        foreach (GameObject p in ActivePlayers)
        {
            PlayerController playscript = p.GetComponent<PlayerController>();
            foreach (GameObject u in PlayerUIs)
            {
                if (u.name == "Player" + playscript.PlayerNumber + "UI")
                {
                    u.SetActive(true);
                }
            }

        }
        InstructText.SetActive(false);
    }

    void checkisdead()
    {
        int deadplayers = 0;
        foreach (GameObject p in ActivePlayers)
        {
            PlayerController playscript = p.GetComponent<PlayerController>();
            if (playscript.isDead)
            {
                deadplayers = deadplayers + 1;
            }

        }
        if (deadplayers == ActivePlayers.Count)
        {
            playersStarting = 0;
            Text messageText = InstructText.GetComponent<Text>();
            messageText.text = "YOU HATH BEEN SLAIN!/n/nHOLD START TO RESET";
            if (Input.GetKey("joystick 1 button 7") && p1In)
            {
                playersStarting = playersStarting + 1;
            }
            if (Input.GetKey("joystick 2 button 7") && p2In)
            {
                playersStarting = playersStarting + 1;
            }
            if (Input.GetKey("joystick 3 button 7") && p3In)
            {
                playersStarting = playersStarting + 1;
            }
            if (Input.GetKey("joystick 4 button 7") && p4In)
            {
                playersStarting = playersStarting + 1;
            }

            if (playersStarting == numPlayers && playersStarting != 0)
            {
                SceneManager.LoadScene(1);
            }
            else {  }
        }
    }
}


//ok what now is left

   

    //nothing indicates to the player that the game has begun/that they can join and that the game has begun and that the players can no longer join.

    //