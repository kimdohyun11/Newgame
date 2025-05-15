using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class PlayerInventory : MonoBehaviour
{
    public List<IItem> Inventory { get; private set; } = new List<IItem>();

    public void AbbInventory(IItem item)
    {
        Inventory.Add(item);
    }

}
