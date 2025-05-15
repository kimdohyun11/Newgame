using UnityEngine;

public class GGMKimbop : MonoBehaviour, IItem
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
        Debug.Log("기분 좋다");
        return true;
    }
}

