using UnityEngine;

public class RainbowPotion : MonoBehaviour, IItem
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
        PlayerStatus player = trget.GetComponentInParent<PlayerStatus>();
        player.Hpmax += 2;
        player.Hp += 2;
        player.Speed = 0.5f;
        PlayerInventory inventory = trget.GetComponentInParent<PlayerInventory>();
        inventory.AbbInventory(this);
        return true;
    }
}
