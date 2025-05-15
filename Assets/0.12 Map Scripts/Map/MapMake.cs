using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public enum Direction {up, down, right, left};
public class Room
{
    public Vector2Int _roomPos;
    public GameObject _roomPrefab;
    public Dictionary<Direction, Room> _connectRoom = new();
    public bool _isBossRoom = false, _isShopRoom = false;
    public bool _isClearRoom = false;
}
public class MapMake : MonoBehaviour
{
    [Header("RoomPrefab")]
    [SerializeField] private GameObject _startRoomPrefab;
    [SerializeField] private GameObject[] _basicRoomtPrefab;
    [SerializeField] private GameObject[] _shopRoomPrefab;
    [SerializeField] private GameObject[] _bossRoomPrefab;

    [Header("RoomSetting")]
    [SerializeField] private int _roomQuantity;
    [SerializeField] private Vector2 _roomSize;

    private Dictionary<Vector2Int, Room> _map = new();
    
    private DoorManage _doorManage;
    public Dictionary<Room, GameObject> RoomObject { get; private set; } = new();
    #region Unity Lifecycle

    private void Awake()
    {
        _doorManage = GameObject.FindFirstObjectByType<DoorManage>().GetComponent<DoorManage>();
    }

    private void Start()
    {
        Vector2Int _startPos = Vector2Int.zero;
        Room _startRoom = new Room { _roomPos = _startPos, _roomPrefab = _startRoomPrefab };
        _map[_startPos] = _startRoom;
        _doorManage.SetStartRoom(_startRoom);
        List<Room> _allRooms = new List<Room> { _map[_startPos] };

        //방들의 위치와 연결을 만들기
        while(true)
        {
            //방이 _roomQuantity 값보다 많다면 종료
            if (_roomQuantity <= _allRooms.Count) break;

            Vector2Int _newPos = new();
            Room _newRoom = new();

            for (int n = Random.Range(0, 3); n < 3; n++)    //하나의 startRoom에서 1~3번 인접 방 만들기
            {
                if (_roomQuantity <= _allRooms.Count) break;

                Direction _roomDir = (Direction)Random.Range(0, 4);
                _newPos = _startPos + ConnectDirection(_roomDir);
                _newRoom = new Room { _roomPos = _newPos, _roomPrefab = _basicRoomtPrefab[Random.Range(0, _basicRoomtPrefab.Length)] };

                //방 연결
                if (_map.ContainsKey(_newPos))
                {
                    _newRoom = _map[_newPos];   //이미 있는 방이면 연결이 꼬이지 않게 그 방의 갑을 받아옴
                    _startRoom._connectRoom[_roomDir] = _newRoom;
                    _newRoom._connectRoom[Opposition(_roomDir)] = _startRoom;
                }
                else
                {
                    _map[_newPos] = _newRoom;    //없던 방이라면 _map에 추가
                    _allRooms.Add(_map[_newPos]);
                    _startRoom._connectRoom[_roomDir] = _newRoom;
                    _newRoom._connectRoom[Opposition(_roomDir)] = _startRoom;
                    
                }
            }
            //startRoom에 값을 변경
            _startRoom = _newRoom;
            _startPos = _newPos;
        }
        //방 구현
        BossRoomSet(_map[Vector2Int.zero], _allRooms);
        ShopRoomSet(_allRooms);
        foreach(var rm in _map)
        {
            Vector3 _actualityVec = new Vector3(rm.Key.x * _roomSize.x, rm.Key.y * _roomSize.y, 0);
            GameObject map = Instantiate(rm.Value._roomPrefab, transform);
            map.transform.position += _actualityVec;
            _doorManage.SeeDoor(rm.Value, map);
            RoomObject[rm.Value] = map;
        }
    }
    #endregion

    #region Method
    private Direction Opposition(Direction roomDir) => roomDir switch 
    {
        
        Direction.up => Direction.down,
        Direction.down => Direction.up,
        Direction.right => Direction.left,
        Direction.left => Direction.right,
        _=>roomDir
    };
    private Vector2Int ConnectDirection(Direction direction) => direction switch
    {
            Direction.up => Vector2Int.up,
            Direction.down => Vector2Int.down,
            Direction.right => Vector2Int.right,
            Direction.left => Vector2Int.left,
        _=> Vector2Int.zero

    };
    private void BossRoomSet(Room startRoom, List<Room> allRoom)
    {
        Room _bossRoom = null;
        float _furthestDist = 0f; //가장 먼 거리

        foreach(Room room in allRoom)
        {
            if (room._connectRoom.Count == 1)
            {
                float _dist = Vector2.Distance(startRoom._roomPos, room._roomPos);
                if (_dist > _furthestDist)
                {
                    _furthestDist = _dist;
                    _bossRoom = room;
                }
            }
        }
        if (_bossRoom == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _bossRoom._isBossRoom = true;
        _bossRoom._roomPrefab = _bossRoomPrefab[Random.Range(0, _bossRoomPrefab.Length)];

        Debug.Log("boos ; " + _bossRoom._roomPos);
    }
    private void ShopRoomSet(List<Room> allRoom)
    {
        List<Room> _candidateRoom = new List<Room>();
        //연결이 하나만 있는 방을 후보로 만들기
        foreach (Room room in allRoom)
        {
            if (room._connectRoom.Count == 1&& !room._isBossRoom && room._roomPos != Vector2Int.zero)
                _candidateRoom.Add(room);
        }

        if (_candidateRoom.Count == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Room _shopRoom = _candidateRoom[Random.Range(0, _candidateRoom.Count)];
        _shopRoom._isShopRoom = true;
        _shopRoom._roomPrefab = _shopRoomPrefab[Random.Range(0, _shopRoomPrefab.Length)];

        Debug.Log("Shop ; " + _shopRoom._roomPos);
    }
    
    #endregion
}
