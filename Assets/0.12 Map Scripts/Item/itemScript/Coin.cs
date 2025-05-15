using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    [SerializeField]private ItemData _itemData;

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
        Debug.Log("aaaaa");
        if (player.Coin + _itemData.Price > player.Coinmax)
            return false;
        player.Coin += _itemData.Price;
        return true;
    }
}
