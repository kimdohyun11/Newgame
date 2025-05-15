using Unity.Cinemachine;
using UnityEngine;

public class CameraManage : MonoBehaviour
{
    private CinemachineConfiner2D _camera;
    [SerializeField] private GameObject _cameraBoundary;

    private void Awake()
    {
        _camera = GameObject.FindFirstObjectByType<CinemachineConfiner2D>().GetComponent<CinemachineConfiner2D>();
    }
    public void CameraSet(Vector2 roomVector)
    {
        _cameraBoundary.transform.position = roomVector;
    }
}
