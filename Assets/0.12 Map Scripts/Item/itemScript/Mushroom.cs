using UnityEngine;

public class Mushroom : MonoBehaviour, IItem
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
        PlayerInventory inventory = trget.GetComponentInParent<PlayerInventory>();
        player.Speed += 1;
        player.gameObject.transform.localScale += new Vector3(1, 1,0);
        inventory.AbbInventory(this);
        return true;
    }
}
