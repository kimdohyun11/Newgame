using System.ComponentModel;
using UnityEngine;

public class StatusItemScript : MonoBehaviour, IItem
{
    [SerializeField] private ItemData itemData;

    public ItemData GetItemData()
    {
        return itemData;
    }

    public void SetitemData(ItemData item)
    {
        itemData = item;
    }

    public bool Use(GameObject trget)
    {
        Debug.Log("»ç¿ë");
        return true;
    }
}
