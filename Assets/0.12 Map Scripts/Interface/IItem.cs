using NUnit.Framework.Interfaces;
using UnityEngine;
public interface IItem
{
    public bool Use(GameObject trget);
    public void SetitemData(ItemData item);
    public ItemData GetItemData();
}
