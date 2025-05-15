using UnityEngine;

public class Hamburger : MonoBehaviour, IItem
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
        player.Hpmax += 4;
        player.Hp += 4;
        player.Speed -= 0.5f;
        PlayerInventory inventory = trget.GetComponentInParent<PlayerInventory>();
        inventory.AbbInventory(this);
        return true;
    }
}
