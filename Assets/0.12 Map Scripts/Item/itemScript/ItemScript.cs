using UnityEngine;
using UnityEngine.Rendering;

public class ItemScript : MonoBehaviour, IItem
{
    [SerializeField] private ItemData _itemData;

    public ItemData GetItemData()
    {
        return _itemData;
    }
    public void SetitemData(ItemData item)
    {
        _itemData = item;
    }
    public bool Use(GameObject trget)
    {
        PlayerStatus player = trget.GetComponent<PlayerStatus>();
        PlayerInventory inventory = trget.GetComponent<PlayerInventory>();
        inventory.AbbInventory(this);
        return true;
    }
}
