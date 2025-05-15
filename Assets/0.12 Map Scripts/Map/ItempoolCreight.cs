using Unity.VisualScripting;
using UnityEngine;

public class ItempoolCreight : MonoBehaviour
{
    [SerializeField] GameObject _itemprefap;
    public GameObject[] Itempool { get; set; } = new GameObject[10];
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject itemObject = GameObject.Instantiate(_itemprefap);
            itemObject.SetActive(false);
            Itempool[i] = itemObject;
        }
    }
}
