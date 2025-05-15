
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject _spwnManager;
    public void StartSpawnManager()
    {
        _spwnManager.SetActive(true);
    }
}
