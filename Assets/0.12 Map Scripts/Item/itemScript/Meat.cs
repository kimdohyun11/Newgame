using UnityEngine;

public class Meat : MonoBehaviour, IItem
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
        player.Hpmax += 2;
        player.Hp += 2;
        PlayerInventory inventory = trget.GetComponent<PlayerInventory>();
        inventory.AbbInventory(this);
        return true;
    }
}