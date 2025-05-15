using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomProportion", menuName = "Scriptable Objects/RoomProportion")]
public class RoomProportion : ScriptableObject
{
    public Roomtype Room;
    public List<AppearItems> itemProportions;
}
[System.Serializable]
public class AppearItems
{
    public RewardType Reward;
    public int Propor;
}