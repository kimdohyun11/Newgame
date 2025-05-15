using UnityEngine;

public class EnemySponPoint : MonoBehaviour
{
    [SerializeField]private GameObject[] _enmyList;
    private void Start()
    {
        GameObject.Instantiate(_enmyList[Random.Range(0, _enmyList.Length)], transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
