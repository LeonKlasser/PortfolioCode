using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    public static RoomDetection Instance { private set; get; }
    [SerializeField] private List<Room> rooms;
    [SerializeField] private bool activateAllRooms;

    public RoomState roomstate;

    public Room currentRoom;
    public float roomToActive;
    public int roomCount;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();
        Room[] roomArray = GetComponentsInChildren<Room>();
        for (int i = 0; i < roomArray.Length; i++)
        {
            rooms.Add(roomArray[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roomstate == RoomState.PlayerEntering)
        {
            if (currentRoom.enemySpawnersInRoom.Count > 0)
            {
                ActivateEnemySpawners();
            }
            else
            {
                roomstate = RoomState.AllEnemiesDead;
            }
        }
        currentRoom.gameObject.transform.Find("lights").gameObject.SetActive(true);
    }

    private void ActivateEnemySpawners()
    {
        foreach (EnemySpawner spawner in currentRoom.enemySpawnersInRoom)
        {
            spawner.gameObject.SetActive(true);
        }
        roomstate = RoomState.SpawningEnemies;
    }

    public void LockDoors(BoxCollider collider)
    {
        collider.enabled = true;
    }

    public void ChangeStateToSpawningEnemies()
    {
        roomstate = RoomState.SpawningEnemies;
    }

    public void ActivateRoom(int number)
    {
        print("Activeteadeda room");
    }

    public int GetRoomNumber(int number)
    {
        return number;
    }
}
