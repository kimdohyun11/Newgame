using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Sprite _basicStand;
    [SerializeField] Sprite _statStand;
    [SerializeField] Sprite _gunstand;
    [SerializeField] TextMeshPro text; 
    private RandomItem _randomItem;
    SpriteRenderer stand;
    SpriteRenderer sprite;
    IItem _currentItem;
    private void Awake()
    {
        _randomItem = FindFirstObjectByType<RandomItem>();
    }
    private void Start()
    {
        stand = GetComponentInParent<SpriteRenderer>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        IItem itemScripte = _randomItem.GetRandomItem(Roomtype.ShopRroom);
        if (itemScripte == null) gameObject.SetActive(false);
        ItemSetting(itemScripte);

    }
    private void ItemSetting(IItem item)
    {
        sprite.sprite = item.GetItemData().Icon;
        stand.sprite = StandSprite(item);
        Component additem = gameObject.AddComponent((item as MonoBehaviour).GetType());
        ((IItem)additem).SetitemData(item.GetItemData());
        _currentItem = (IItem)additem;
        text.text = _currentItem.GetItemData().Price.ToString();
    }
    private Sprite StandSprite(IItem item) => item.GetItemData().type switch
    {
        ItemType.stat => _statStand,
        _ => _basicStand
    };
}
