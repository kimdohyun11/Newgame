using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public string Name;
    public Sprite Icon;
    public ItemGrade Grade;
    public bool IsOverItem;
    public int Price;
}

public enum ItemGrade { Normal, Rair, Epic, Boss, Shop }