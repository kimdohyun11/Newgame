using UnityEngine;

public class HpPotion_M : MonoBehaviour,IItem
{
    [SerializeField] private int _hpRecoveryStat = 2;
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
        if (player.Hp == player.Hpmax) return false;

        player.Hp += _hpRecoveryStat;
        return true;
    }
}
