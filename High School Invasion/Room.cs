using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region SerializeFields
    [HideInInspector]
    public bool isExitRoom;

    [HideInInspector]
    public MoveUpStairs moveUpStairsScript;

    [HideInInspector] 
    public Transform cameraPos;

    [HideInInspector]
    public bool hasEnemySpawners;

    [HideInInspector]
    [SerializeField] public List<EnemySpawner> enemySpawnersInRoom;

    [HideInInspector]
    public List<EnemyScript> enemiesInRoom;

    [HideInInspector]
    [SerializeField] public BreackableDoor[] doors;

    [HideInInspector]
    public bool hasMoreThenOneExit;

    [HideInInspector]
    public int roomToActivate;

    [HideInInspector]
    public int firstRoomToActivate;

    [HideInInspector]
    public int secondRoomToActivate;

    [HideInInspector]
    public int roomNumber;
    #endregion

    private float timer;

    [HideInInspector]
    public bool allEnemiesDied;

    [HideInInspector]
    public bool playerHasEnteredBefore;

    private RoomDetection roomDetection;

    private void Start()
    {
        roomDetection = RoomDetection.Instance;
        if (hasEnemySpawners)
        {
            enemySpawnersInRoom = new List<EnemySpawner>();
            EnemySpawner[] enemySpawners = GetComponentsInChildren<EnemySpawner>();
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawnersInRoom.Add(enemySpawners[i]);
            }

        }

        for (int i = 0; i < enemySpawnersInRoom.Count; i++)
        {
            enemySpawnersInRoom[i].gameObject.SetActive(false);
        }
        doors = GetComponentsInChildren<BreackableDoor>();
    }

    private void Update()
    {
        if (roomDetection.roomstate == RoomState.PlayerEntering || roomDetection.roomstate == RoomState.SpawningEnemies)
        {
            timer += Time.deltaTime;
            /*if (hasEnemySpawners)
            {
                foreach (EnemySpawner spawner in enemySpawnersInRoom)
                {
                    spawner.gameObject.SetActive(true);
                }
            }*/

            if (timer > 1)
            {
                if (enemiesInRoom.Count <= 0)
                {
                    //roomDetection.roomstate = RoomState.AllEnemiesDead;
                }
                timer = 0;
            }
        }

        if (enemiesInRoom.Count > 0)
        {
            allEnemiesDied = false;
            //roomDetection.roomstate = RoomState.SpawningEnemies;
        }
        else
        {
            if (roomDetection.roomstate == RoomState.AllEnemiesDead)
            {
                //roomDetection.currentRoom.allEnemiesDied = true;
            }

            if (!roomDetection.currentRoom.hasEnemySpawners)
            {
                if (roomDetection.roomstate != RoomState.PlayerEntering && roomDetection.roomstate != RoomState.SpawningEnemies)
                {
                    //roomDetection.roomstate = RoomState.AllEnemiesDead;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomDetection.roomstate = RoomState.PlayerEntering;
            roomDetection.currentRoom = gameObject.GetComponentInParent<Room>();

            if (isExitRoom)
            {
                moveUpStairsScript.enabled = true;
                GetComponent<MoveUpStairs>().player = other.GetComponent<Transform>();
                other.GetComponent<PlayerController>().playerControls.Controls.Disable();
                other.GetComponent<PlayerController>().constantYPos = 0;
                Transform cameraHolder = GameObject.Find("CameraHolder").transform;

                cameraHolder.parent = null;
                cameraHolder.position = cameraPos.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<RoomDetection>().roomstate = RoomState.PlayerLeaving;
        }
    }

    public void EnemyKilled(EnemyScript enemy)
    {
        enemiesInRoom.Remove(enemy);
        if (enemiesInRoom.Count < 1)
        {
            GetComponentInParent<RoomDetection>().roomstate = RoomState.AllEnemiesDead;
        }
    }
}