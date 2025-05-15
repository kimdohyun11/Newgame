using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGrideProportion", menuName = "Scriptable Objects/ItemGrideProportion")]
public class ItemGrideProportion : ScriptableObject
{
    public ItemType WhatItemType;
    public List<GradeWeight> Weight;
}
[System.Serializable]
public class GradeWeight
{
    public ItemGrade Grade;
    public int Weight;
}
