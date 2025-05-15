using System;
using System.Net.NetworkInformation;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorBase : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    private DoorManage _manager;
    
    private void Awake()
    {
        _manager = GameObject.FindFirstObjectByType<DoorManage>().GetComponent<DoorManage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.CompareTag("Player"));
        if (collision.CompareTag("Player"))
        {
            _manager.RoomMove(collision, _direction);
            Debug.Log("ddd"+ collision.name+"dddd"+_direction.ToString());
        }
    }
}
