using UnityEngine;

public class DoorManage : MonoBehaviour
{
    [SerializeField] private Vector2 _roomSize;
    private MapMake _roomMaker;
    private Room _currentRoom;
    private CameraManage _camera;
    private bool _roomLock = false;

    private void Awake()
    {
        _roomMaker = GameObject.FindFirstObjectByType<MapMake>().GetComponent<MapMake>();
        _camera = GameObject.FindFirstObjectByType<CameraManage>().GetComponent<CameraManage>();
    }
    public void SetStartRoom(Room room)
    {
        _currentRoom = room;
    }

    public void RoomMove(Collider2D collision, Direction doorDir)
    {
        Room _nextRoom;
        if (_currentRoom._connectRoom.ContainsKey(doorDir))
        {
            if (!_roomLock)
            {
                _nextRoom = _currentRoom._connectRoom[doorDir];
                Vector2 _nextRoomPos = _nextRoom._roomPos;
                collision.transform.position = _nextRoomPos * _roomSize + DoorExit(doorDir);
                _camera.CameraSet(_nextRoomPos * _roomSize);
                _currentRoom = _nextRoom;
            }
        }
        if (!_currentRoom._isClearRoom)
        {
            if (_roomMaker.RoomObject[_currentRoom].TryGetComponent<RoomManager>(out RoomManager roomManager))
            {
                _roomLock = true;
                roomManager.StartSpawnManager();
            }
        }
    }
    private Vector2 DoorExit(Direction direction) => direction switch
    {
        Direction.up => Vector2.down * 3f,
        Direction.down => Vector2.up * 3f,
        Direction.right => Vector2.left * 7f,
        Direction.left => Vector2.right * 7f,
        _ => Vector2.zero
    };

    public void SeeDoor(Room room, GameObject map)
    {
        Doors _doors = map.GetComponentInChildren<Doors>();
        foreach(var cr in room._connectRoom)
        {
            _doors.GetDoors(cr.Key).SetActive(false);
            if (room._isBossRoom || cr.Value._isBossRoom)
                _doors.BossDoorsOn(cr.Key);
        }
    }
    public void CurrentRoomClear()
    {
        _currentRoom._isClearRoom = true;
        _roomLock = false;
    }


}
