using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("DoorWall")]
    [SerializeField] private GameObject _upDoorWall;
    [SerializeField] private GameObject _downDoorWall;
    [SerializeField] private GameObject _rightDoorWall;
    [SerializeField] private GameObject _leftDoorWall;

    [Header("BossDoor")]
    [SerializeField] private GameObject _upBossDoor;
    [SerializeField] private GameObject _downBossDoor;
    [SerializeField] private GameObject _rightBossDoor;
    [SerializeField] private GameObject _leftBossDoor;

    private bool _roomLock = false;
    public void SetLock()
    {
        _roomLock = !_roomLock;
    }
    public GameObject GetDoors(Direction dir) => dir switch
    {
        Direction.up => _upDoorWall,
        Direction.down => _downDoorWall,
        Direction.right => _rightDoorWall,
        Direction.left => _leftDoorWall,
        _ => null
    };
    public void BossDoorsOn(Direction dir)
    {
        switch (dir)
        {
        case Direction.up: _upBossDoor.SetActive(true); break;
        case Direction.down: _downBossDoor.SetActive(true); break;
        case Direction.right: _rightBossDoor.SetActive(true); break;
        case Direction.left: _leftBossDoor.SetActive(true); break;
        default : Debug.Log("Null"); break;
        }
    }

}
